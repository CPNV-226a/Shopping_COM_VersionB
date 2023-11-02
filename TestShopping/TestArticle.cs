using Shopping;
using static Shopping.Article;

namespace TestShopping
{
    public class TestArticle
    {
        #region private attributes
        private Article? _article = null;
        private int _id;
        private string _description;
        private float _price = 0f;
        #endregion private attributes

        [SetUp]
        public void Setup()
        {
            _id = 1;
            _description = "product description";
            _price = 20.45f;
            _article = new Article(_id, _description, _price);
        }

        [Test]
        public void AllProperties_AfterInstantiation_Success()
        {
            //given
            //refer to Setup

            //when
            //Event will be triggered by constructor

            //then
            Assert.That(_article.Id, Is.EqualTo(_id));
            Assert.That(_article.Description, Is.EqualTo(_description));
            Assert.That(_article.Price, Is.EqualTo(_price));
        }

        #region Description
        /*
         * A article description must meet theses specifications:
         *  * 2 words minimum
         *  * No special chars like {!,*,+,/}
         *  * Max 50 chars (blank spaces count)
         */

        //TODO add test case for constructor

        [Test]
        public void Description_ShortDescription_ReturnNewValue()
        {
            //given
            string expectedDescription = "After Shave";

            //when
            _article.Description = expectedDescription;

            //then
            Assert.That(_article.Description, Is.EqualTo(expectedDescription));
        }

        [Test]
        public void Description_LongDescription_ReturnNewValue()
        {
            //given
            string expectedDescription = "A very long long long long long long descriptionn";

            //when
            _article.Description = expectedDescription;

            //then
            Assert.That(_article.Description, Is.EqualTo(expectedDescription));
        }

        [Test]
        public void Description_SingleWordDescription_ThrowException()
        {
            //given

            //when
            Assert.Throws<TooShortDescriptionException>(() => _article.Description = "TooShort");

            //then
            //throws exception
        }

        [Test]
        public void Description_DescriptionContainingSpecialChars_ThrowException()
        {
            //given

            //when
            Assert.Throws<SpecialCharInDescriptionException>(() => _article.Description = "Jacques+Daniel");

            //then
            //throws exception
        }

        [Test]
        public void Description_TooLongDescription_ThrowException()
        {
            //given

            //when
            Assert.Throws<TooLongDescriptionException>(() => _article.Description = "A very very very very very looonnng descriptioooooon");

            //then
            //throws exception
        }
        #endregion Description

        #region ToString
        [Test]
        public void ToString_WithPrice_GetArticleInStringFormat()
        {
            //given
            string expectedString = "Votre article [id " + _id + "] [description " + _description + "] [prix " + _price + "]";

            //when

            //then
            Assert.AreEqual(expectedString, _article.ToString());
        }

        [Test]
        public void ToString_WithoutPrice_GetArticleInStringFormat()
        {
            //given
            string expectedString = "Votre article [id " + _id + "] [description " + _description + "]";

            //when

            //then
            Assert.AreEqual(expectedString, _article.ToString(false));
        }
        #endregion ToString
    }
}