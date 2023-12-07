using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMarket.Data;
using ProductMarket.Interfaces;
using ProductMarket.Models;
using ProductMarket.ViewModels;

namespace ProductMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IProductRepository productRepository, IPhotoService photoService,
            IHttpContextAccessor httpContextAccessor)
        {

            _productRepository = productRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _productRepository.GetAll();
            return View(products);
        }

        public async Task<IActionResult> Detail(int id)
        {

            Product product = await _productRepository.GetByIdAsync(id);
            return View(product);
        }
        public IActionResult Create()
        {
            var currUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createProductViewModel = new CreateProductViewModel
            {
                AppUserId = currUserId
            };
            return View(createProductViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(productVM.Image);

                var product = new Product
                {
                    ProductName = productVM.ProductName,
                    ProductDescription = productVM.ProductDescription,
                    Image = result.Url.ToString(),
                    ProductPrice = productVM.ProductPrice,
                    ProductCategory = productVM.ProductCategory,
                    AppUserId = productVM.AppUserId,
                    Address = new Address
                    {
                        City = productVM.Address.City,
                        State = productVM.Address.State,
                        PostalCode = productVM.Address.PostalCode,
                        Street = productVM.Address.Street
                    }
                };
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(productVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return View("Error");
            }
            var productVM = new EditProductViewModel
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                URL = product.Image,
                ProductPrice = product.ProductPrice,
                ProductCategory = product.ProductCategory,
                AddressId = product.AddressId,
                Address = product.Address

            };
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductViewModel productVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failure to edit");
                return View("Edit", productVM);
            }
            var userProduct = await _productRepository.GetByIdAsyncNoTracking(id);
            if (userProduct != null) {
                try
                {
                    await _photoService.DeletePhotoAsync(userProduct.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Couldn't delete photo");
                    return View(productVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(productVM.Image);
                var product = new Product
                    {
                        ProductId = id,
                        ProductName = productVM.ProductName,
                        ProductDescription = productVM.ProductDescription,
                        Image = photoResult.Url.ToString(),
                        ProductPrice = productVM.ProductPrice,
                        ProductCategory = productVM.ProductCategory,
                        AddressId = productVM.AddressId,
                        Address = productVM.Address
                    };
                _productRepository.Update(product);

                return RedirectToAction("Index");

            }
            else
            {
                return View(productVM);
            }

           
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _productRepository.GetByIdAsync(id);
            if (productDetails == null)
            {
                return View("Error");
            }
            return View(productDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productDetails = await _productRepository.GetByIdAsync(id);

            if (productDetails == null)
            {
                return View("Error");
            }

            _productRepository.Delete(productDetails);
            return RedirectToAction("Index");
        }
    }
}
