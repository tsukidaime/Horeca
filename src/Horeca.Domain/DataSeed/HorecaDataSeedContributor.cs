using Horeca.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Horeca.DataSeed
{
    public class HorecaDataSeedContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<Category, Guid> _productRepository;
        private readonly IRepository<Category, Guid> _productBidRepository;
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;


        public HorecaDataSeedContributor(
            IRepository<Category, Guid> categoryRepository,
            IRepository<Category, Guid> productRepository,
            IRepository<Category, Guid> productBidRepository,
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator)
        {
            _currentTenant = currentTenant;
            _categoryRepository = categoryRepository;
            _productBidRepository = productBidRepository;
            _productRepository = productRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                if (await _categoryRepository.CountAsync() <= 0)
                    await SeedCategories();
            }
        }
        private async Task SeedCategories()
        {
            var categoriesToInsert = new List<Category>(){
                new Category
                {
                    Name = "Vegetables"
                },
                new Category
                {
                    Name = "Fruit"
                },
                new Category
                {
                    Name = "Meat"
                },
                new Category
                {
                    Name = "Bird"
                },
                new Category
                {
                    Name = "Fish and seafood"
                },
                new Category
                {
                    Name = "Milk products"
                },
            };
            var categories = new List<Category>();
            foreach (var category in categoriesToInsert)
                categories.Add(await _categoryRepository.InsertAsync(category));

            var vegetables = new List<Category>()
            {
                new Category{
                    ParentId = categories[0].Id,
                    Name = "Roots"
                },
                new Category{
                    ParentId = categories[0].Id,
                    Name = "Tomatoes"
                },
                new Category{
                    ParentId = categories[0].Id,
                    Name = "Mushrooms"
                },
                new Category{
                    ParentId = categories[0].Id,
                    Name = "Cabbage"
                },
                new Category{
                    ParentId = categories[0].Id,
                    Name = "Cucumbers"
                },
            };

            var fruits = new List<Category>()
            {
                new Category{
                    ParentId = categories[1].Id,
                    Name = "Apples"
                },
                new Category{
                    ParentId = categories[1].Id,
                    Name = "Grape"
                },
                new Category{
                    ParentId = categories[1].Id,
                    Name = "Berries"
                },
                new Category{
                    ParentId = categories[1].Id,
                    Name = "Exotic"
                },
                new Category{
                    ParentId = categories[1].Id,
                    Name = "Citruses"
                },
            };

            var meet = new List<Category>()
            {
                new Category{
                    ParentId = categories[2].Id,
                    Name = "Frozen beef"
                },
                new Category{
                    ParentId = categories[2].Id,
                    Name = "Chilled beef"
                },
                new Category{
                    ParentId = categories[2].Id,
                    Name = "Mutton"
                },
                new Category{
                    ParentId = categories[2].Id,
                    Name = "Horsemeat"
                },
                new Category{
                    ParentId = categories[2].Id,
                    Name = "Pork"
                },
            };

            var bird = new List<Category>()
            {
                new Category{
                    ParentId = categories[3].Id,
                    Name = "Chicken"
                },
                new Category{
                    ParentId = categories[3].Id,
                    Name = "Turkey"
                },
                new Category{
                    ParentId = categories[3].Id,
                    Name = "Duck, goose"
                },
                new Category{
                    ParentId = categories[3].Id,
                    Name = "Quail"
                }
            };

            var fish = new List<Category>()
            {
                new Category{
                    ParentId = categories[4].Id,
                    Name = "Fish"
                },
                new Category{
                    ParentId = categories[4].Id,
                    Name = "Seafood"
                },
            };

            var milk = new List<Category>()
            {
                new Category{
                    ParentId = categories[5].Id,
                    Name = "Milk"
                },
                new Category{
                    ParentId = categories[5].Id,
                    Name = "Sour cream"
                },
                new Category{
                    ParentId = categories[5].Id,
                    Name = "Butter"
                },
                new Category{
                    ParentId = categories[5].Id,
                    Name = "Cheese"
                },
                new Category{
                    ParentId = categories[5].Id,
                    Name = "Eggs"
                },
            };

            await _categoryRepository.InsertManyAsync(vegetables);
            await _categoryRepository.InsertManyAsync(fruits);
            await _categoryRepository.InsertManyAsync(meet);
            await _categoryRepository.InsertManyAsync(bird);
            await _categoryRepository.InsertManyAsync(fish);
            await _categoryRepository.InsertManyAsync(milk);
        }
    }
}
