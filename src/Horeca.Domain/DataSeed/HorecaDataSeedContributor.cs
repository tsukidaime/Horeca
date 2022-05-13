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
                    Name = "Овощи"
                },
                new Category
                {
                    Name = "Фрукты"
                },
                new Category
                {
                    Name = "Мясо"
                },
                new Category
                {
                    Name = "Птица"
                },
                new Category
                {
                    Name = "Рыба и морепродукты"
                },
                new Category
                {
                    Name = "Молочная продукция"
                },
            };
            var categories = new List<Category>();
            foreach (var category in categoriesToInsert)
                categories.Add(await _categoryRepository.InsertAsync(category));

            var vegetables = new List<Category>()
            {
                new Category{
                    ParentId = categories[0].Id,
                    Name = "Корнеплоды"
                },
                new Category{
                    ParentId = categories[0].Id,
                    Name = "Помидоры"
                },
                new Category{
                    ParentId = categories[0].Id,
                    Name = "Грибы"
                },
                new Category{
                    ParentId = categories[0].Id,
                    Name = "Капуста"
                },
                new Category{
                    ParentId = categories[0].Id,
                    Name = "Огурцы"
                },
            };

            var fruits = new List<Category>()
            {
                new Category{
                    ParentId = categories[1].Id,
                    Name = "Яблоки"
                },
                new Category{
                    ParentId = categories[1].Id,
                    Name = "Виноград"
                },
                new Category{
                    ParentId = categories[1].Id,
                    Name = "Ягоды"
                },
                new Category{
                    ParentId = categories[1].Id,
                    Name = "Экзотические"
                },
                new Category{
                    ParentId = categories[1].Id,
                    Name = "Цитрусы"
                },
            };

            var meet = new List<Category>()
            {
                new Category{
                    ParentId = categories[2].Id,
                    Name = "Говядина замороженная"
                },
                new Category{
                    ParentId = categories[2].Id,
                    Name = "Говядина охлажденная"
                },
                new Category{
                    ParentId = categories[2].Id,
                    Name = "Баранина"
                },
                new Category{
                    ParentId = categories[2].Id,
                    Name = "Конина"
                },
                new Category{
                    ParentId = categories[2].Id,
                    Name = "Свинина"
                },
            };

            var bird = new List<Category>()
            {
                new Category{
                    ParentId = categories[3].Id,
                    Name = "Курица"
                },
                new Category{
                    ParentId = categories[3].Id,
                    Name = "Индейка"
                },
                new Category{
                    ParentId = categories[3].Id,
                    Name = "Утка, гусь"
                },
                new Category{
                    ParentId = categories[3].Id,
                    Name = "Перепелка"
                }
            };

            var fish = new List<Category>()
            {
                new Category{
                    ParentId = categories[4].Id,
                    Name = "Рыба"
                },
                new Category{
                    ParentId = categories[4].Id,
                    Name = "Морепродукты"
                },
            };

            var milk = new List<Category>()
            {
                new Category{
                    ParentId = categories[5].Id,
                    Name = "Молоко"
                },
                new Category{
                    ParentId = categories[5].Id,
                    Name = "Сметана"
                },
                new Category{
                    ParentId = categories[5].Id,
                    Name = "Масло"
                },
                new Category{
                    ParentId = categories[5].Id,
                    Name = "Сыр"
                },
                new Category{
                    ParentId = categories[5].Id,
                    Name = "Яйца"
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
