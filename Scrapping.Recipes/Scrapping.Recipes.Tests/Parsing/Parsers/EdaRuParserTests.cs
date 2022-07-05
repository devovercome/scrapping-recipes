namespace Scrapping.Recipes.Tests
{
    [TestFixture]
    public class EdaRuParserTests
    {
        [Test]
        public void Parse_ShouldWork_ForSampleCase()
        {
            var url = "https://eda.ru/recepty/osnovnye-blyuda/ovoshhnoe-ragu-s-kapustoj-34016";
            var expected = new RecipeModel()
            {
                Title = "Îâîùíîå ðàãó ñ êàïóñòîé",
                ImageUrl = "https://eda.ru/img/eda/c620x415/s1.eda.ru/StaticContent/Photos/120928105641/140825202502/p_O.jpg",
                NutrientsModel = new()
                {
                    Calories = 232,
                    Proteins = 8,
                    Fat = 8,
                    Carbon = 33
                },
                CookTimeMinutes = 45,
                FoodCategoriesModel = new(new string[] { "Îñíîâíûå áëþäà", "ÎÂÎÙÍÎÅ ÐÀÃÓ Ñ ÊÀÁÀ×ÊÀÌÈ", "ÎÂÎÙÍÎÅ ÐÀÃÓ", "ÐÀÃÓ" }),
                DishesCount = 2,
                Ingredients = new IngredientModel[9],
                Steps = new string[8],
                ProviderName = "eda.ru",
                RecipeUrl = url
            };

            
            var recipe = new EdaRuParser().Parse(url);
            
            // TODO: fix this mess.
            Assert.Multiple(() =>
            {
                Assert.That(recipe.ImageUrl, Is.EqualTo(expected.ImageUrl));

                Assert.That(recipe.NutrientsModel.Calories, Is.EqualTo(expected.NutrientsModel.Calories));
                Assert.That(recipe.NutrientsModel.Proteins, Is.EqualTo(expected.NutrientsModel.Proteins));
                Assert.That(recipe.NutrientsModel.Fat, Is.EqualTo(expected.NutrientsModel.Fat));
                Assert.That(recipe.NutrientsModel.Carbon, Is.EqualTo(expected.NutrientsModel.Carbon));

                Assert.That(recipe.CookTimeMinutes, Is.EqualTo(expected.CookTimeMinutes));
                Assert.That(recipe.FoodCategoriesModel.Categories.Count(), Is.EqualTo(expected.FoodCategoriesModel.Categories.Count()));
                Assert.That(recipe.FoodCategoriesModel.MainCategory, Is.EqualTo(expected.FoodCategoriesModel.MainCategory));
                Assert.That(recipe.DishesCount, Is.EqualTo(expected.DishesCount));
                Assert.That(recipe.Ingredients.Count(), Is.EqualTo(expected.Ingredients.Count()));
                Assert.That(recipe.Steps.Count(), Is.EqualTo(expected.Steps.Count()));

                Assert.That(recipe.ProviderName, Is.EqualTo(expected.ProviderName));
                Assert.That(recipe.RecipeUrl, Is.EqualTo(expected.RecipeUrl));
            });
        }
    }
}