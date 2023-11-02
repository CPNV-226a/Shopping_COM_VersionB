using Shopping;
using static Shopping.Cart;
using static Shopping.CartItem;

namespace TestShopping
{
    public class TestCart
    {
        #region private attributes
        private Cart? _cart = null;
        #endregion private attributes

        [SetUp]
        public void Setup()
        {
            _cart = new Cart();
        }

        [Test]
        public void Add_FirstSingleCartItem_Success()
        {
            //given
            //refer to Setup
            int expectedArticlesQuantity = 1;
            List<Article> expectedArticles = ArticleGenerator.Generate(expectedArticlesQuantity);

            int expectedArticleInCartItem = 1;
            CartItem expectedCartItem = new CartItem(expectedArticles[0], expectedArticleInCartItem);
            List<CartItem> expectedCartItems = new List<CartItem> { expectedCartItem };
            Assert.That(_cart.CartItems.Count(), Is.EqualTo(0));

            //when
            _cart.Add(expectedCartItems);

            //then
            Assert.That(_cart.CartItems.Count(), Is.EqualTo(expectedArticlesQuantity));
            Assert.That(_cart.CartItems, Is.EqualTo(expectedCartItems));
        }

        [Test]
        public void Add_MultipleSingleCartItems_Success()
        {
            //given
            //refer to Setup
            int expectedArticlesQuantity = 2;
            List<Article> expectedArticles = ArticleGenerator.Generate(expectedArticlesQuantity);

            int expectedQuantity1 = 1;
            CartItem expectedCartItem1 = new CartItem(expectedArticles[0], expectedQuantity1);

            int expectedQuantity2 = 1;
            CartItem expectedCartItem2 = new CartItem(expectedArticles[1], expectedQuantity2);

            List<CartItem> expectedCartItems = new List<CartItem> { expectedCartItem1, expectedCartItem2 };
            Assert.That(_cart.CartItems.Count(), Is.EqualTo(0));

            //when
            _cart.Add(expectedCartItems);

            //then
            Assert.That(_cart.CartItems.Count(), Is.EqualTo(expectedQuantity1 + expectedQuantity2));
            Assert.That(_cart.CartItems, Is.EqualTo(expectedCartItems));
        }

        [Test]
        public void Add_OneMultipleCartItems_Success()
        {
            //given
            //refer to Setup
            int expectedArticlesQuantity = 1;
            List<Article> expectedArticles = ArticleGenerator.Generate(expectedArticlesQuantity);

            int expectedArticleInCartItem = 2;
            CartItem expectedCartItem = new CartItem(expectedArticles[0], expectedArticleInCartItem);
            List<CartItem> expectedCartItems = new List<CartItem> { expectedCartItem };
            Assert.That(_cart.CartItems.Count(), Is.EqualTo(0));

            //when
            _cart.Add(expectedCartItems);

            //then
            Assert.That(_cart.CartItems.Count(), Is.EqualTo(expectedArticlesQuantity));
            Assert.That(_cart.CartItems, Is.EqualTo(expectedCartItems));
        }

        //TODO add remove test cases

        [Test]
        public void Price_EmptyCart_GetPrice()
        {
            //given
            float expectedPrice = 0.00f;

            //when

            //then
            Assert.That(_cart.Price(), Is.EqualTo(expectedPrice));
        }

        [Test]
        public void Price_NotEmptyCart_GetPrice()
        {
            //given
            List<Article> articles = ArticleGenerator.Generate(5);
            List<CartItem> cartItems = new List<CartItem>();
            foreach (Article article in articles)
            {
                cartItems.Add(new CartItem(article, 1));
            }
            float expectedPrice = 30.00f;
            _cart.Add(cartItems);

            //when

            //then
            Assert.That(_cart.Price(), Is.EqualTo(expectedPrice));
        }

        [Test]
        public void PriceAverage_UniqueValue_GetAverage()
        {
            //given
            List<Article> articles = ArticleGenerator.Generate(5);
            List<CartItem> cartItems = new List<CartItem>();
            foreach (Article article in articles)
            {
                cartItems.Add(new CartItem(article, 1));
            }
            _cart.Add(cartItems);

            //when

            //then
            Assert.That(_cart.Price(true), Is.EqualTo(6));
        }

        // TODO Add price test case for multiple article (quantity > 1)

        [Test]
        public void DoesExist_ById_True()
        {
            //given
            List<Article> articles = ArticleGenerator.Generate(10);
            List<CartItem> cartItems = new List<CartItem>();
            foreach (Article article in articles)
            {
                cartItems.Add(new CartItem(article, 1));
            }
            _cart.Add(cartItems);

            //when            

            //then
            Assert.IsTrue(_cart.DoesExist(10));
        }

        [Test]
        public void DoesExist_ById_False()
        {
            //given
            List<Article> articles = ArticleGenerator.Generate(10);
            List<CartItem> cartItems = new List<CartItem>();
            foreach (Article article in articles)
            {
                cartItems.Add(new CartItem(article, 1));
            }
            _cart.Add(cartItems);

            //when

            //then
            Assert.IsFalse(_cart.DoesExist(999));
        }

        [Test]
        public void Cheapest_UniqueValue_GetArticleId()
        {
            //given
            List<Article> articles = ArticleGenerator.Generate(10);
            List<CartItem> cartItems = new List<CartItem>();
            foreach (Article article in articles)
            {
                cartItems.Add(new CartItem(article, 1));
            }
            _cart.Add(cartItems);

            //when

            //then
            Assert.That(_cart.Cheapest(), Is.EqualTo(1));
        }

        [Test]
        public void MostExpensive_UniqueValue_GetArticleId()
        {
            //given
            List<Article> articles = ArticleGenerator.Generate(10);
            List<CartItem> cartItems = new List<CartItem>();
            foreach (Article article in articles)
            {
                cartItems.Add(new CartItem(article, 1));
            }
            _cart.Add(cartItems);

            //when

            //then
            Assert.That(_cart.MostExpensive(), Is.EqualTo(10));
        }

        #region GetCartItemsByMinPrice
        [Test]
        public void GetCartItemsByMinPrice_ItemsFound_GetListOfItems()
        {
            //given
            int expectedAmountOfArticleItemsFound = 6;
            List<Article> articles = ArticleGenerator.Generate(10);
            List<CartItem> cartItems = new List<CartItem>();
            foreach (Article article in articles)
            {
                cartItems.Add(new CartItem(article, 1));
            }
            _cart.Add(cartItems);

            List<CartItem> expectedListOfCartItem = new List<CartItem>();

            //when


            //then
            Assert.AreEqual(expectedAmountOfArticleItemsFound, _cart.GetCartItemsByMinPrice(10.00f).Count);
        }

        [Test]
        public void GetCartItemsByPrice_NoItemNotFound_GetNull()
        {
            //given
            List<Article> articles = ArticleGenerator.Generate(10);
            List<CartItem> cartItems = new List<CartItem>();
            foreach (Article article in articles)
            {
                cartItems.Add(new CartItem(article, 1));
            }
            _cart.Add(cartItems);

            //when

            //then
            Assert.AreEqual(null, _cart.GetCartItemsByMinPrice(10000.00f));
        }

        [Test]
        public void GetCartItemsByPrice_EmptyCart_ThrowException()
        {
            //given

            //when
            Assert.Throws<EmptyCartException>(() => _cart.GetCartItemsByMinPrice(10000.00f));

            //then
            //throws exception
        }
        #endregion GetCartItemsByMinPrice
    }
}