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
    }
}
