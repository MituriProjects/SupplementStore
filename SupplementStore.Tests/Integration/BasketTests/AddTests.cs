﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.BasketTests {

    [TestClass]
    public class AddTests : IntegrationTestsBase {

        [TestMethod]
        public async Task DefaultReturnUrl_RedirectsToIndex() {

            await PostAsync("/Basket/Add", new Dictionary<string, string> {
                { "ProductId",  Guid.NewGuid().ToString()},
                { "Quantity", "1"}
            }, TestData.User);

            ExamineRedirect("/Basket");
        }

        [TestMethod]
        public async Task PassedReturnUrl_RedirectsToPassedReturnUrl() {

            await PostAsync("/Basket/Add", new Dictionary<string, string> {
                { "ProductId",  Guid.NewGuid().ToString()},
                { "Quantity", "1"},
                { "ReturnUrl", "/Product"}
            }, TestData.User);

            ExamineRedirect("/Product");
        }

        [TestMethod]
        public async Task ProductIdIsValidAndQuantityEqualsOne_InsertsBasketProduct() {

            var product = TestProduct.Random();

            await PostAsync("/Basket/Add", new Dictionary<string, string> {
                { "ProductId",  product.Id.ToString()},
                { "Quantity", "1"}
            }, TestData.User);

            TestDocument<BasketProduct>.Single(e => e.UserId == TestData.User.Id && e.ProductId == product.Id && e.Quantity == 1);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task ProductIdIsValidAndQuantityEqualsTwo_InsertsBasketProducts() {

            var product = TestProduct.Random();

            await PostAsync("/Basket/Add", new Dictionary<string, string> {
                { "ProductId",  product.Id.ToString()},
                { "Quantity", "2"}
            }, TestData.User);

            TestDocument<BasketProduct>.Single(e => e.UserId == TestData.User.Id && e.ProductId == product.Id && e.Quantity == 2);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task ProductIdIsValidAndQuantityEqualsOneAndBasketProductExists_IncreasesBasketProductQuantity() {

            var product = TestProduct.Random();
            TestBasketProduct.Random()
                .WithUserId(TestData.User.Id)
                .WithProductId(product.Id)
                .WithQuantity(1);

            await PostAsync("/Basket/Add", new Dictionary<string, string> {
                { "ProductId",  product.Id.ToString()},
                { "Quantity", "2"}
            }, TestData.User);

            TestDocument<BasketProduct>.Single(e => e.UserId == TestData.User.Id && e.ProductId == product.Id && e.Quantity == 3);
            TestDocument<BasketProduct>.Single(e => e.UserId == TestData.User.Id && e.ProductId == product.Id);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task UserIsLoggedOut_NoBasketProductCreation() {

            var product = TestProduct.Random();

            await PostAsync("/Basket/Add", new Dictionary<string, string> {
                { "ProductId",  product.Id.ToString()},
                { "Quantity", "1"}
            });

            TestDocument<BasketProduct>.None(e => e.ProductId == product.Id);
        }

        [TestMethod]
        public async Task ProductIdIsInvalid_NoBasketProductCreation() {

            await PostAsync("/Basket/Add", new Dictionary<string, string> {
                { "ProductId",  "InvalidId"},
                { "Quantity", "1"}
            }, TestData.User);

            TestDocument<BasketProduct>.None(e => e.UserId == TestData.User.Id);
        }

        [TestMethod]
        public async Task NoProductWithSentId_NoBasketProductCreation() {

            await PostAsync("/Basket/Add", new Dictionary<string, string> {
                { "ProductId",  Guid.NewGuid().ToString()},
                { "Quantity", "1"}
            }, TestData.User);

            TestDocument<BasketProduct>.None(e => e.UserId == TestData.User.Id);
        }

        [TestMethod]
        public async Task QuantityEqualsZero_NoBasketProductCreation() {

            var product = TestProduct.Random();

            await PostAsync("/Basket/Add", new Dictionary<string, string> {
                { "ProductId",  product.Id.ToString()},
                { "Quantity", "0"}
            }, TestData.User);

            TestDocument<BasketProduct>.None(e => e.UserId == TestData.User.Id && e.ProductId == product.Id);
        }

        [TestMethod]
        public async Task QuantityIsBelowZero_NoBasketProductCreation() {

            var product = TestProduct.Random();

            await PostAsync("/Basket/Add", new Dictionary<string, string> {
                { "ProductId",  product.Id.ToString()},
                { "Quantity", "-1"}
            }, TestData.User);

            TestDocument<BasketProduct>.None(e => e.UserId == TestData.User.Id && e.ProductId == product.Id);
        }
    }
}
