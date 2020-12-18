using Logic;
using Models;
using Moq;
using NUnit.Framework;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    [TestFixture]
    public class LogicTest
    {
        [Test] // Crud: Add Price_SortType
        public void TestAddPrice_SortType()
        {
            Mock<IRepository<Price_SortType>> price_sorttypeRepo = new Mock<IRepository<Price_SortType>>();
            price_sorttypeRepo.Setup(repo => repo.Add(It.IsAny<Price_SortType>()));
            Price_SortTypeLogic price_SortTypeLogic = new Price_SortTypeLogic(price_sorttypeRepo.Object);
            Price_SortType price_SortTypeItem = new Price_SortType();

            price_SortTypeLogic.AddPrice_SortType(price_SortTypeItem);

            price_sorttypeRepo.Verify(repo => repo.Add(price_SortTypeItem), Times.Once);
            price_sorttypeRepo.Verify(repo => repo.Read(), Times.Never);
            price_sorttypeRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            price_sorttypeRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            price_sorttypeRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Price_SortType>()), Times.Never);
        }

        [Test] // Crud: GetAll ItemsCount_SortType
        public void TestGetAllItemsCount_SortyType()
        {
            Mock<IRepository<ItemsCount_SortType>> itemscRepo = new Mock<IRepository<ItemsCount_SortType>>();
            List<ItemsCount_SortType> itemsCount_SortTypes = new List<ItemsCount_SortType>()
            {
                new ItemsCount_SortType() { Id = "asdfghjkl123" },
                new ItemsCount_SortType() { Id = "923843298gh" },
                new ItemsCount_SortType() { Id = "trthorthothof" },
                new ItemsCount_SortType() { Id = "fejwnfjkwn" },
                new ItemsCount_SortType() { Id = "asdassdasad" }
            };
            itemscRepo.Setup(repo => repo.Read()).Returns(itemsCount_SortTypes.AsQueryable());
            ItemsCount_SortTypeLogic itemsCount_SortTypeLogic = new ItemsCount_SortTypeLogic(itemscRepo.Object);

            var result = itemsCount_SortTypeLogic.GetAllItemsCount_SortType();

            Assert.That(result.Count, Is.EqualTo(itemsCount_SortTypes.Count));
            Assert.That(result, Is.EquivalentTo(itemsCount_SortTypes));

            itemscRepo.Verify(repo => repo.Read(), Times.Once);
            itemscRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            itemscRepo.Verify(repo => repo.Add(It.IsAny<ItemsCount_SortType>()), Times.Never);
            itemscRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            itemscRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<ItemsCount_SortType>()), Times.Never);
        }

        [Test] // Crud: GetOne Category
        public void TestGetCategory()
        {
            Mock<IRepository<Category>> categoryRepo = new Mock<IRepository<Category>>();
            Mock<IRepository<Product>> productRepo = new Mock<IRepository<Product>>();
            Mock<IRepository<ProductCategory>> productCategRepo = new Mock<IRepository<ProductCategory>>();
            categoryRepo.Setup(repo => repo.Read(It.IsAny<string>())).Returns(new Category() { Id = "asdassdasad" });
            Category expectedCategory = new Category() { Id = "asdassdasad" };
            CategoryLogic categoryLogic = new CategoryLogic(categoryRepo.Object, productRepo.Object, productCategRepo.Object);

            var result = categoryLogic.GetCategory("asdassdasad");

            Assert.That(result, Is.EqualTo(expectedCategory));

            categoryRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Once);
            categoryRepo.Verify(repo => repo.Read(), Times.Never);
            categoryRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            categoryRepo.Verify(repo => repo.Add(It.IsAny<Category>()), Times.Never);
            categoryRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Category>()), Times.Never);

            productRepo.Verify(repo => repo.Read(), Times.Never);
            productRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            productRepo.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Never);
            productRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            productRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Product>()), Times.Never);

            productCategRepo.Verify(repo => repo.Read(), Times.Never);
            productCategRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            productCategRepo.Verify(repo => repo.Add(It.IsAny<ProductCategory>()), Times.Never);
            productCategRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            productCategRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<ProductCategory>()), Times.Never);
        }

        [Test] // Crud: Delete Price_SortType
        public void TestDeletePrice_SortType()
        {
            Mock<IRepository<Price_SortType>> price_sorttypeRepo = new Mock<IRepository<Price_SortType>>();
            price_sorttypeRepo.Setup(repo => repo.Delete(It.IsAny<string>()));
            Price_SortTypeLogic price_SortTypeLogic = new Price_SortTypeLogic(price_sorttypeRepo.Object);

            price_SortTypeLogic.DeletePrice_SortType("asdasdasd");

            price_sorttypeRepo.Verify(repo => repo.Add(It.IsAny<Price_SortType>()), Times.Never);
            price_sorttypeRepo.Verify(repo => repo.Read(), Times.Never);
            price_sorttypeRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            price_sorttypeRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Once);
            price_sorttypeRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Price_SortType>()), Times.Never);
        }

        [Test] // Crud: Update ItemsCount_SortType
        public void TestUpdateItemsCount_SortType()
        {
            Mock<IRepository<ItemsCount_SortType>> itemscRepo = new Mock<IRepository<ItemsCount_SortType>>();
            itemscRepo.Setup(repo => repo.Update(It.IsAny<string>(), It.IsAny<ItemsCount_SortType>()));
            ItemsCount_SortTypeLogic itemsCount_SortTypeLogic = new ItemsCount_SortTypeLogic(itemscRepo.Object);

            itemsCount_SortTypeLogic.UpdateItemsCount_SortType("dsksdgsdgkk", new ItemsCount_SortType());

            itemscRepo.Verify(repo => repo.Read(), Times.Never);
            itemscRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            itemscRepo.Verify(repo => repo.Add(It.IsAny<ItemsCount_SortType>()), Times.Never);
            itemscRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            itemscRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<ItemsCount_SortType>()), Times.Once);
        }

        [Test] // Non-Crud: GetProductCategory()
        public void TestGetProductCategory()
        {
            Mock<IRepository<Category>> categoryRepo = new Mock<IRepository<Category>>(MockBehavior.Loose);
            Mock<IRepository<Product>> productRepo = new Mock<IRepository<Product>>(MockBehavior.Loose);
            Mock<IRepository<ProductCategory>> productCategRepo = new Mock<IRepository<ProductCategory>>(MockBehavior.Loose);
            List<ProductCategory> productCategs = new List<ProductCategory>()
            {
                new ProductCategory() { CategoryId = "asdfghjkl123", ProductId = "qwertzuiop456" },
                new ProductCategory() { CategoryId = "923843298gh", ProductId = "tzpojpmtj" },
                new ProductCategory() { CategoryId = "trthorthothof", ProductId = "hjk,áhkjh" },
                new ProductCategory() { CategoryId = "fejwnfjkwn", ProductId = "qwertzuiop456" },
                new ProductCategory() { CategoryId = "asdassdasad", ProductId = "qwertzuiop456" },
                new ProductCategory() { CategoryId = "j24jk23jk4j6b", ProductId = "mmhmghjfphdgs" }
            };
            List<Category> categories = new List<Category>()
            {
                new Category() { Id = "asdfghjkl123" },
                new Category() { Id = "923843298gh" },
                new Category() { Id = "trthorthothof" },
                new Category() { Id = "fejwnfjkwn" },
                new Category() { Id = "asdassdasad" },
                new Category() { Id = "j24jk23jk4j6b" },
                new Category() { Id = "dwmfnaifgpabigb" },
                new Category() { Id = "923hh239h3ths" }
            };
            List<Category> expectedCategories = new List<Category>()
            {
                new Category() { Id = "asdfghjkl123" },
                new Category() { Id = "fejwnfjkwn" },
                 new Category() { Id = "asdassdasad" }
            };
            productCategRepo.Setup(repo => repo.Read()).Returns(productCategs.AsQueryable());
            categoryRepo.Setup(repo => repo.Read()).Returns(categories.AsQueryable());
            CategoryLogic categoryLogic = new CategoryLogic(categoryRepo.Object, productRepo.Object, productCategRepo.Object);

            var result = categoryLogic.GetProductCategories("qwertzuiop456");

            Assert.That(result.Count, Is.EqualTo(expectedCategories.Count));
            Assert.That(result, Is.EquivalentTo(expectedCategories));

            productCategRepo.Verify(repo => repo.Read(), Times.Once);
            productCategRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            productCategRepo.Verify(repo => repo.Add(It.IsAny<ProductCategory>()), Times.Never);
            productCategRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            productCategRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<ProductCategory>()), Times.Never);

            categoryRepo.Verify(repo => repo.Read(), Times.Once);
            categoryRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            categoryRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            categoryRepo.Verify(repo => repo.Add(It.IsAny<Category>()), Times.Never);
            categoryRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Category>()), Times.Never);

            productRepo.Verify(repo => repo.Read(), Times.Never);
            productRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            productRepo.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Never);
            productRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            productRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Product>()), Times.Never);
        }

        [Test] // Non-Crud: SortByProductLine()
        public void TestSortByProductLine()
        {
            Mock<IRepository<Category>> categoryRepo = new Mock<IRepository<Category>>(MockBehavior.Loose);
            Mock<IRepository<Product>> productRepo = new Mock<IRepository<Product>>(MockBehavior.Loose);
            Mock<IRepository<ProductCategory>> productCategRepo = new Mock<IRepository<ProductCategory>>(MockBehavior.Loose);
            Mock<IRepository<Price_SortType>> price_SortRepo = new Mock<IRepository<Price_SortType>>();
            Mock<IRepository<ItemsCount_SortType>> itemsc_SortRepo = new Mock<IRepository<ItemsCount_SortType>>();
            Mock<IRepository<FiguresCount_SortType>> figuresc_SortRepo = new Mock<IRepository<FiguresCount_SortType>>();
            Mock<IRepository<Age_SortType>> age_SortRepo = new Mock<IRepository<Age_SortType>>();
            ProductLogic productLogic = new ProductLogic(productRepo.Object, price_SortRepo.Object, itemsc_SortRepo.Object, figuresc_SortRepo.Object, categoryRepo.Object, age_SortRepo.Object, productCategRepo.Object);
            List<Product> products = new List<Product>()
            {
                new Product(){ ProductLine = "qweqwe" },
                new Product(){ ProductLine = "rtzrtz" },
                new Product(){ ProductLine = "asdasd" },
                new Product(){ ProductLine = "asdasd" },
                new Product(){ ProductLine = "qweqwe" },
                new Product(){ ProductLine = "rtzrtz" },
                new Product(){ ProductLine = "iopiop" },
                new Product(){ ProductLine = "jkljkl" },
                new Product(){ ProductLine = "bnmbnm" },
                new Product(){ ProductLine = "cvbcvb" }
            };
            List<string> productLines = new List<string>()
            {
                "asdasd", "bnmbnm", "rtzrtz"
            };
            List<Product> expectedproducts = new List<Product>()
            {
                new Product(){ ProductLine = "rtzrtz" },
                new Product(){ ProductLine = "asdasd" },
                new Product(){ ProductLine = "asdasd" },
                new Product(){ ProductLine = "rtzrtz" },
                new Product(){ ProductLine = "bnmbnm" }
            };

            var result = productLogic.SortByProductLine(products, productLines);

            Assert.That(result.Count, Is.EqualTo(expectedproducts.Count));
            Assert.That(result, Is.EquivalentTo(expectedproducts));

            productRepo.Verify(repo => repo.Read(), Times.Never);
            productRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            productRepo.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Never);
            productRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            productRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Product>()), Times.Never);

            categoryRepo.Verify(repo => repo.Read(), Times.Never);
            categoryRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            categoryRepo.Verify(repo => repo.Add(It.IsAny<Category>()), Times.Never);
            categoryRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            categoryRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Category>()), Times.Never);

            productCategRepo.Verify(repo => repo.Read(), Times.Never);
            productCategRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            productCategRepo.Verify(repo => repo.Add(It.IsAny<ProductCategory>()), Times.Never);
            productCategRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            productCategRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<ProductCategory>()), Times.Never);
        }

        [Test] // Non-Crud: SortByItemsCount()
        public void TestSortByItemsCount()
        {
            Mock<IRepository<Category>> categoryRepo = new Mock<IRepository<Category>>(MockBehavior.Loose);
            Mock<IRepository<Product>> productRepo = new Mock<IRepository<Product>>(MockBehavior.Loose);
            Mock<IRepository<ProductCategory>> productCategRepo = new Mock<IRepository<ProductCategory>>(MockBehavior.Loose);
            Mock<IRepository<Price_SortType>> price_SortRepo = new Mock<IRepository<Price_SortType>>();
            Mock<IRepository<ItemsCount_SortType>> itemsc_SortRepo = new Mock<IRepository<ItemsCount_SortType>>();
            Mock<IRepository<FiguresCount_SortType>> figuresc_SortRepo = new Mock<IRepository<FiguresCount_SortType>>();
            Mock<IRepository<Age_SortType>> age_SortRepo = new Mock<IRepository<Age_SortType>>();
            ProductLogic productLogic = new ProductLogic(productRepo.Object, price_SortRepo.Object, itemsc_SortRepo.Object, figuresc_SortRepo.Object, categoryRepo.Object, age_SortRepo.Object, productCategRepo.Object);
            List<Product> products = new List<Product>()
            {
                new Product(){ ItemsCount_SortTypeId = "qweqwe" },
                new Product(){ ItemsCount_SortTypeId = "rtzrtz" },
                new Product(){ ItemsCount_SortTypeId = "asdasd" },
                new Product(){ ItemsCount_SortTypeId = "asdasd" },
                new Product(){ ItemsCount_SortTypeId = "qweqwe" },
                new Product(){ ItemsCount_SortTypeId = "rtzrtz" },
                new Product(){ ItemsCount_SortTypeId = "iopiop" },
                new Product(){ ItemsCount_SortTypeId = "jkljkl" },
                new Product(){ ItemsCount_SortTypeId = "bnmbnm" },
                new Product(){ ItemsCount_SortTypeId = "cvbcvb" }
            };
            List<string> itemscount_sorttypeIds = new List<string>()
            {
                "asdasd", "bnmbnm", "rtzrtz"
            };
            List<Product> expectedproducts = new List<Product>()
            {
                new Product(){ ItemsCount_SortTypeId = "rtzrtz" },
                new Product(){ ItemsCount_SortTypeId = "asdasd" },
                new Product(){ ItemsCount_SortTypeId = "asdasd" },
                new Product(){ ItemsCount_SortTypeId = "rtzrtz" },
                new Product(){ ItemsCount_SortTypeId = "bnmbnm" }
            };

            var result = productLogic.SortByItemsCount(products, itemscount_sorttypeIds);

            Assert.That(result.Count, Is.EqualTo(expectedproducts.Count));
            Assert.That(result, Is.EquivalentTo(expectedproducts));

            productRepo.Verify(repo => repo.Read(), Times.Never);
            productRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            productRepo.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Never);
            productRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            productRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Product>()), Times.Never);

            categoryRepo.Verify(repo => repo.Read(), Times.Never);
            categoryRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            categoryRepo.Verify(repo => repo.Add(It.IsAny<Category>()), Times.Never);
            categoryRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            categoryRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<Category>()), Times.Never);

            productCategRepo.Verify(repo => repo.Read(), Times.Never);
            productCategRepo.Verify(repo => repo.Read(It.IsAny<string>()), Times.Never);
            productCategRepo.Verify(repo => repo.Add(It.IsAny<ProductCategory>()), Times.Never);
            productCategRepo.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
            productCategRepo.Verify(repo => repo.Update(It.IsAny<string>(), It.IsAny<ProductCategory>()), Times.Never);
        }
    }
}
