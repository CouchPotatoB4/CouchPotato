using Microsoft.VisualStudio.TestTools.UnitTesting;
using CouchPotato.Backend.ApiUtil.Aniflix;
using System;
using System.Collections.Generic;
using System.Text;

namespace CouchPotatoTests.Backend.ApiUtil.Aniflix
{
    [TestClass()]
    public class JsonsTest
    {
        [TestMethod()]
        public void ShowJsonTest()
        {
            ShowJson showJson = new ShowJson();
            showJson.airing = 1;
            showJson.id = 100;
            showJson.name = "Jason";
            showJson.name_alt = "JasonOld";
            showJson.description = "Description";
            showJson.cover_landscape = "cover1";
            showJson.cover_landscape_original = "cover2";
            showJson.cover_portrait = "cover3";
            showJson.created_at = "0";
            showJson.deleted_at = "1";
            showJson.url = "url";
            showJson.visible_since = "visible";
            showJson.updated_at = "update";
            showJson.howManyAbos = 2;
            showJson.seasonCount = 3;
            showJson.rating = "Great";

            Assert.AreEqual(1, showJson.airing);
            Assert.AreEqual(100, showJson.id);
            Assert.AreEqual("Jason", showJson.name);
            Assert.AreEqual("JasonOld", showJson.name_alt);
            Assert.AreEqual("Description", showJson.description);
            Assert.AreEqual("cover1", showJson.cover_landscape);
            Assert.AreEqual("cover2", showJson.cover_landscape_original);
            Assert.AreEqual("cover3", showJson.cover_portrait);
            Assert.AreEqual("0", showJson.created_at);
            Assert.AreEqual("1", showJson.deleted_at);
            Assert.AreEqual("url", showJson.url);
            Assert.AreEqual("visible", showJson.visible_since);
            Assert.AreEqual("update", showJson.updated_at);
            Assert.AreEqual(2, showJson.howManyAbos);
            Assert.AreEqual(3, showJson.seasonCount);
            Assert.AreEqual("Great", showJson.rating);
        }

        [TestMethod()]
        public void GenreWithShowsTest()
        {
            
        }
    }
}
