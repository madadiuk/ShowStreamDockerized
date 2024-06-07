using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestingGenresManagement
{
    [TestClass]
    public class GenresManagerTests
    {
        private GenresManager genresManager;
        private clsDataConnection db;

        [TestInitialize]
        public void Setup()
        {
            genresManager = new GenresManager();
            db = new clsDataConnection();
            SeedDatabase();
        }

        [TestCleanup]
        public void Teardown()
        {
            CleanDatabase();
        }

        private void SeedDatabase()
        {
            db.Execute("spCleanAndReseedTblGenres");
        }

        private void CleanDatabase()
        {
            var cleanDb = new clsDataConnection();
            cleanDb.Execute("spCleanTblVideoFiles");
            cleanDb.Execute("spCleanTblDownloads");
            cleanDb.Execute("spCleanTblEpisodes");
            cleanDb.Execute("spCleanTblSeries");
            cleanDb.Execute("spCleanTblMovies");
            cleanDb.Execute("spCleanTblGenres");
        }

        #region Genre Name Field Tests

        [TestMethod]
        public void AddGenre_Name_MinMinusOne_ShouldFail()
        {
            string error = genresManager.AddGenre("", "Valid description");
            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Name_MinBoundary_ShouldPass()
        {
            string error = genresManager.AddGenre("A", "Valid description");
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Name_MinPlusOne_ShouldPass()
        {
            string error = genresManager.AddGenre("AB", "Valid description");
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Name_MaxMinusOne_ShouldPass()
        {
            string name = new string('a', 49);
            string error = genresManager.AddGenre(name, "Valid description");
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Name_MaxBoundary_ShouldPass()
        {
            string name = new string('a', 50);
            string error = genresManager.AddGenre(name, "Valid description");
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Name_MaxPlusOne_ShouldFail()
        {
            string name = new string('a', 51);
            string error = genresManager.AddGenre(name, "Valid description");
            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Name_Mid_ShouldPass()
        {
            string name = new string('a', 25);
            string error = genresManager.AddGenre(name, "Valid description");
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Name_ExtremeMax_ShouldFail()
        {
            string name = new string('a', 500);
            string error = genresManager.AddGenre(name, "Valid description");
            Assert.AreNotEqual(string.Empty, error);
        }

        #endregion

        #region Genre Description Field Tests

        [TestMethod]
        public void AddGenre_Description_ValidData_ShouldPass()
        {
            string error = genresManager.AddGenre("Valid name", "Action Movies");
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Description_MinLength_ShouldPass()
        {
            string error = genresManager.AddGenre("Valid name", "A");
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Description_MaxLength_ShouldPass()
        {
            string description = new string('a', 255);
            string error = genresManager.AddGenre("Valid name", description);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Description_Empty_ShouldPass()
        {
            string error = genresManager.AddGenre("Valid name", "");
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Description_MaxMinusOne_ShouldPass()
        {
            string description = new string('a', 254);
            string error = genresManager.AddGenre("Valid name", description);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Description_MaxBoundary_ShouldPass()
        {
            string description = new string('a', 255);
            string error = genresManager.AddGenre("Valid name", description);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Description_MaxPlusOne_ShouldFail()
        {
            string description = new string('a', 256);
            string error = genresManager.AddGenre("Valid name", description);
            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Description_Mid_ShouldPass()
        {
            string description = new string('a', 125);
            string error = genresManager.AddGenre("Valid name", description);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddGenre_Description_ExtremeMax_ShouldFail()
        {
            string description = new string('a', 500);
            string error = genresManager.AddGenre("Valid name", description);
            Assert.AreNotEqual(string.Empty, error);
        }

        #endregion

        #region Update and Delete Genre Tests

        [TestMethod]
        public void UpdateGenre_ValidData_ShouldPass()
        {
            genresManager.AddGenre("Action", "Action Movies");
            int genreID = GetLastInsertedGenreID();

            string error = genresManager.UpdateGenre(genreID, "Comedy", "Comedy Movies");
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void DeleteGenre_ValidID_ShouldPass()
        {
            genresManager.AddGenre("Action", "Action Movies");
            int genreID = GetLastInsertedGenreID();

            string error = genresManager.DeleteGenre(genreID);
            Assert.AreEqual(string.Empty, error);
        }

       // [TestMethod]
       // public void DeleteGenre_WithMovies_ShouldFail()
       // {
       //    genresManager.AddGenre("Action", "Action Movies");
       //     int genreID = GetLastInsertedGenreID();

            // Simulate adding a movie with this genre
      //      clsDataConnection addMovieDb = new clsDataConnection();
       //     addMovieDb.AddParameter("@Title", "Sample Movie");
       //     addMovieDb.AddParameter("@Description", "Sample Description");
       //     addMovieDb.AddParameter("@GenreID", genreID);
      //      addMovieDb.AddParameter("@Director", "Sample Director");
      //      addMovieDb.AddParameter("@ReleaseDate", DateTime.Now);
     //       addMovieDb.AddParameter("@Duration", 120);
     //       addMovieDb.Execute("spAddMovie");
//
    //        string error = genresManager.DeleteGenre(genreID);
    //        Assert.AreEqual("Cannot delete genre. There are movies associated with this genre. Delete the movies first.", error);
        //}

        #endregion

        private int GetLastInsertedGenreID()
        {
            db.Execute("spGetLastInsertedGenreID");
            if (db.Count > 0)
            {
                return Convert.ToInt32(db.DataTable.Rows[0]["GenreID"]);
            }
            return -1;
        }
    }
}
