using System.Collections;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using NUnit.Framework;
using UnityEngine;

public class TheTest : MonoBehaviour {

    [Test]
    public void Run()
    {
        //------------------------------------------------------
        // ReSharper runner may not be working with Unity tests,
        // run it from Unity3d UI instead.
        //------------------------------------------------------
        string dbFile = Path.GetTempFileName();
        try
        {
            using (var db = new LiteDatabase(dbFile))
            {
                // Get customer collection
                var collection = db.GetCollection<Data>("data");

                // Create your new customer instance
                var item = new Data
                {
                    Id = 1,
                    Text = "Something",
                    Number = 10
                };

                // Insert new customer document (Id will be auto-incremented)
                collection.Insert(item);

                // Update a document inside a collection
                item.Text = "Anything";
                collection.Update(item);

                // Print all elements
                foreach (var element in collection.FindAll())
                {
                    Debug.Log(element);
                }

                // This works
                Assert.NotNull(collection.FindById(new BsonValue(1)));

                // Index document using a document property
                collection.EnsureIndex(x => x.Text);

                // Use Linq to query documents
                var result = collection.FindOne(x => x.Text.StartsWith("A"));
                // This fails
                Assert.NotNull(result);
                Assert.AreEqual(10, result.Number);
                Assert.AreEqual("Anything", result.Text);
            }
        }
        finally
        {
            File.Delete(dbFile);
        }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Text: {1}, Number: {2}", Id, Text, Number);
        }
    }
}
