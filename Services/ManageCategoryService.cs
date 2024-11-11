using e_commerce_website.Data;
using e_commerce_website.Enums;
using e_commerce_website.Exceptions;
using e_commerce_website.Helper.Category;
using e_commerce_website.Helper;
using e_commerce_website.Models;
using e_commerce_website.Services.Interfaces;
using e_commerce_website.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_website.Services
{
    public class ManageCategoryService : IManageCategoryService
    {
        private readonly ShopDbContext _context;
        //constructor
        public ManageCategoryService(ShopDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CategoryCreateRequest request)
        {
            var category = new Category()
            {
                generalityName = request.generalityName,
                name = request.name,
                status = request.status
            };
            _context.categories.Add(category);
            await _context.SaveChangesAsync();
            return category.id;
        }



        public async Task<List<CategoryViewModel>> GetAll()
        {

            return await _context.categories.Where(i => i.status == ActionStatus.Display).Select(rs => new CategoryViewModel
            {
                id = rs.id,
                name = rs.name,
                generalityName = rs.generalityName,
                status = rs.status,
                Products = rs.Products.Where(p => p.status == ActionStatus.Display).ToList()
            }).ToListAsync();
        }

        public async Task<CategoryViewModel> getCategoryById(int categoryId)
        {
            var category = await _context.categories.Where(i => i.status == ActionStatus.Display)
                .Select(ele => new CategoryViewModel()
                {
                    id = ele.id,
                    name = ele.name,
                    generalityName = ele.generalityName,
                    status = ele.status
                })
                .FirstOrDefaultAsync(x => x.id == categoryId);

            return category;
        }

        public async Task<int> Update(CategoryUpdateRequest request)
        {
            var category = new Category()
            {
                id = request.id,
                name = request.name,
                generalityName = request.generalityName,
                status = request.status,

            };

            _context.Entry(category).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int categoryId)
        {
            var category = await _context.categories.FindAsync(categoryId);
            if (category == null)
            {
                throw new ShopException($"Cannot find any category to this category id {categoryId}!");
            }
            //đổi cờ ko xóa
            category.status = ActionStatus.Deleted;
            _context.Entry(category).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        //
        public async Task<List<CategoryViewModel>> Search(string search)
        {
            var data = await _context.categories.ToListAsync();

            data = data.Where(ele => FormatVietnamese.convertToUnSign(ele.name.ToLower())
           .Contains(FormatVietnamese.convertToUnSign(search.ToLower())) ||
           FormatVietnamese.convertToUnSign(ele.generalityName.ToLower())
           .Contains(FormatVietnamese.convertToUnSign(search.ToLower()))
           ).ToList();

            return data.Where(i => i.status == ActionStatus.Display).Select(rs => new CategoryViewModel
            {
                id = rs.id,
                name = rs.name,
                generalityName = rs.generalityName,
                status = rs.status,
                Products = rs.Products.Where(p => p.status == ActionStatus.Display).ToList()
            }).ToList();
        }

        public async Task<CategoryViewModel> GetById(int categoryId)
        {
            var category = await _context.categories
                .Where(i => i.status == ActionStatus.Display && i.id == categoryId)
                .Select(ele => new CategoryViewModel()
                {
                    id = ele.id,
                    name = ele.name,
                    generalityName = ele.generalityName,
                    status = ele.status,
                    Products = ele.Products.Where(p => p.status == ActionStatus.Display).ToList()
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                throw new ShopException($"Category with ID {categoryId} not found.");
            }

            return category;  
        }
    }
}
