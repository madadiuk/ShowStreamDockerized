using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestingMoviesManagement
{
    [TestClass]
    public class MoviesManagerTests
    {
        private MoviesManager moviesManager;
        private clsDataConnection db;

        [TestInitialize]
        public void Setup()
        {
            moviesManager = new MoviesManager();
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
            db.Execute("spCleanTblVideoFiles");
            db.Execute("spCleanTblDownloads");
            db.Execute("spCleanTblEpisodes");
            db.Execute("spCleanTblSeries");
            db.Execute("spCleanAndReseedTblMovies");
        }

        private void CleanDatabase()
        {
            db.Execute("spCleanTblVideoFiles");
            db.Execute("spCleanTblDownloads");
            db.Execute("spCleanTblEpisodes");
            db.Execute("spCleanTblSeries");
            db.Execute("spCleanTblMovies");
        }

        #region Title Field Tests

        [TestMethod]
        public void AddMovie_Title_MinMinusOne_ShouldFail()
        {
            string error = moviesManager.AddMovie("", "Valid description", 1, "Director", DateTime.Now, 120);
            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Title_MinBoundary_ShouldPass()
        {
            string error = moviesManager.AddMovie("A", "Valid description", 1, "Director", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Title_MinPlusOne_ShouldPass()
        {
            string error = moviesManager.AddMovie("AB", "Valid description", 1, "Director", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Title_MaxMinusOne_ShouldPass()
        {
            string title = new string('a', 99);
            string error = moviesManager.AddMovie(title, "Valid description", 1, "Director", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Title_MaxBoundary_ShouldPass()
        {
            string title = new string('a', 100);
            string error = moviesManager.AddMovie(title, "Valid description", 1, "Director", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Title_MaxPlusOne_ShouldFail()
        {
            string title = new string('a', 101);
            string error = moviesManager.AddMovie(title, "Valid description", 1, "Director", DateTime.Now, 120);
            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Title_ExtremeMax_ShouldFail()
        {
            string title = new string('a', 500);
            string error = moviesManager.AddMovie(title, "Valid description", 1, "Director", DateTime.Now, 120);
            Assert.AreNotEqual(string.Empty, error);
        }

        #endregion

        #region Description Field Tests

        [TestMethod]
        public void AddMovie_Description_ValidData_ShouldPass()
        {
            string error = moviesManager.AddMovie("Valid title", "Action Movies", 1, "Director", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Description_MinLength_ShouldPass()
        {
            string error = moviesManager.AddMovie("Valid title", "A", 1, "Director", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Description_MaxLength_ShouldPass()
        {
            string description = new string('a', 1000);
            string error = moviesManager.AddMovie("Valid title", description, 1, "Director", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Description_Empty_ShouldPass()
        {
            string error = moviesManager.AddMovie("Valid title", "", 1, "Director", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Description_MaxPlusOne_ShouldFail()
        {
            string description = new string('a', 1001);
            string error = moviesManager.AddMovie("Valid title", description, 1, "Director", DateTime.Now, 120);
            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Description_ExtremeMax_ShouldFail()
        {
            string description = new string('a', 5000);
            string error = moviesManager.AddMovie("Valid title", description, 1, "Director", DateTime.Now, 120);
            Assert.AreNotEqual(string.Empty, error);
        }

        #endregion

        #region Director Field Tests

        [TestMethod]
        public void AddMovie_Director_MinMinusOne_ShouldFail()
        {
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, "", DateTime.Now, 120);
            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Director_MinBoundary_ShouldPass()
        {
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, "A", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Director_MinPlusOne_ShouldPass()
        {
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, "AB", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Director_MaxMinusOne_ShouldPass()
        {
            string director = new string('a', 99);
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, director, DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Director_MaxBoundary_ShouldPass()
        {
            string director = new string('a', 100);
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, director, DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Director_MaxPlusOne_ShouldFail()
        {
            string director = new string('a', 101);
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, director, DateTime.Now, 120);
            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Director_ExtremeMax_ShouldFail()
        {
            string director = new string('a', 500);
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, director, DateTime.Now, 120);
            Assert.AreNotEqual(string.Empty, error);
        }

        #endregion

        #region ReleaseDate Field Tests

        [TestMethod]
        public void AddMovie_ReleaseDate_PastDate_ShouldPass()
        {
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, "Director", new DateTime(2000, 1, 1), 120);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_ReleaseDate_FutureDate_ShouldPass()
        {
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, "Director", new DateTime(2100, 1, 1), 120);
            Assert.AreEqual(string.Empty, error);
        }

        #endregion

        #region Duration Field Tests

        [TestMethod]
        public void AddMovie_Duration_Zero_ShouldFail()
        {
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, "Director", DateTime.Now, 0);
            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Duration_Negative_ShouldFail()
        {
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, "Director", DateTime.Now, -120);
            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void AddMovie_Duration_Positive_ShouldPass()
        {
            string error = moviesManager.AddMovie("Valid title", "Valid description", 1, "Director", DateTime.Now, 120);
            Assert.AreEqual(string.Empty, error);
        }

        #endregion

        #region Update and Delete Movie Tests

        [TestMethod]
        public void UpdateMovie_ValidData_ShouldPass()
        {
            moviesManager.AddMovie("Valid title", "Valid description", 1, "Director", DateTime.Now, 120);
            int movieID = GetLastInsertedMovieID();

            string error = moviesManager.UpdateMovie(movieID, "Updated title", "Updated description", 1, "Updated Director", DateTime.Now, 130);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void DeleteMovie_ValidID_ShouldPass()
        {
            moviesManager.AddMovie("Valid title", "Valid description", 1, "Director", DateTime.Now, 120);
            int movieID = GetLastInsertedMovieID();

            string error = moviesManager.DeleteMovie(movieID);
            Assert.AreEqual(string.Empty, error);
        }

       /* [TestMethod]
        public void DeleteMovie_WithVideoFiles_ShouldFail()
        {
            moviesManager.AddMovie("Valid title", "Valid description", 1, "Director", DateTime.Now, 120);
            int movieID = GetLastInsertedMovieID();

            // Simulate adding a video file with this movie
            clsDataConnection addVideoFileDb = new clsDataConnection();
            addVideoFileDb.AddParameter("@MovieID", movieID);
            addVideoFileDb.AddParameter("@FilePath", "SamplePath");
            addVideoFileDb.AddParameter("@SeriesID", 1);  // Ensure SeriesID is provided
            addVideoFileDb.Execute("spAddVideoFile");

            string error = moviesManager.DeleteMovie(movieID);
            Assert.AreEqual("Cannot delete movie. There are video files associated with this movie. Delete the video files first.", error);
        }
       */
        #endregion

        private int GetLastInsertedMovieID()
        {
            db.Execute("spGetLastInsertedMovieID");
            if (db.Count > 0)
            {
                return Convert.ToInt32(db.DataTable.Rows[0]["MovieID"]);
            }
            return -1;
        }
    }
}
