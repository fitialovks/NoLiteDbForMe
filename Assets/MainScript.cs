using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public Text ResultText;

    public void CheckDb()
    {
        ResultText.text = "Test failed";
        string dbFile = Application.persistentDataPath + "/temp.litedb";
        try
        {
            using (var db = new LiteDatabase(dbFile))
            {
                // Get customer collection
                var collection = db.GetCollection<Entity>("data");

                // Create your new customer instance
                var item = new Entity
                {
                    Id = 1,
                    Text = "Something",
                    Positions = new[] {Vector3.up, Vector3.down,}
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
                Assert(collection.FindById(new BsonValue(1)) != null);

                // Index document using a document property
                collection.EnsureIndex(x => x.Text);

                // Use Linq to query documents
                var result = collection.FindOne(x => x.Text.StartsWith("A"));
                // This fails
                Assert(result != null);
                Assert("Anything" == result.Text);
            }
            ResultText.text = "Test succeeded";
        }
        finally
        {
            File.Delete(dbFile);
        }
    }

    private void Assert(bool condition)
    {
        Debug.Assert(condition);
        if (!condition)
        {
            throw new Exception();
        }
    }
}