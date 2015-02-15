using Govision.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Govision.database
{
    class HistoryDatabase
    {
        IsolatedStorageFile IsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

        public HistoryList GetHistoryList()
        {
            HistoryList HistoryListItems = new HistoryList();

            if (IsolatedStorage.FileExists("HistoryDB"))
            {
                //Get a list of all history items
                using (IsolatedStorageFileStream fileStream = IsolatedStorage.OpenFile("HistoryDB", FileMode.Open))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(HistoryList));
                    HistoryListItems = (HistoryList)serializer.ReadObject(fileStream);
                }
            }

            return HistoryListItems;
        }

        public void AddItem(HistoryData item)
        {

            HistoryList HistoryListItems = new HistoryList();

            //Check if there is a history database in IsolatedStorage (is there is a history list?) (if not so the history is empty!)
            if (IsolatedStorage.FileExists("HistoryDB"))
            {
                //Get a list of all history items
                using (IsolatedStorageFileStream fileStream = IsolatedStorage.OpenFile("HistoryDB", FileMode.Open))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(HistoryList));
                    HistoryListItems = (HistoryList)serializer.ReadObject(fileStream);
                }
            }

            HistoryListItems.Insert(0,item); //Add the item to the top of the list

            // Store the new list of history items to the IsolatedStorage

            if (IsolatedStorage.FileExists("HistoryDB"))
            {
                IsolatedStorage.DeleteFile("HistoryDB");
            }

            using (IsolatedStorageFileStream fileStream = IsolatedStorage.OpenFile("HistoryDB", FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(HistoryList));
                serializer.WriteObject(fileStream, HistoryListItems);
            }
        }

        public void UpdateDatabase(HistoryList list)
        {

            if (IsolatedStorage.FileExists("HistoryDB"))
            {
                IsolatedStorage.DeleteFile("HistoryDB");
            }

            using (IsolatedStorageFileStream fileStream = IsolatedStorage.OpenFile("HistoryDB", FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(HistoryList));
                serializer.WriteObject(fileStream, list);
            }
        }

        public void RemoveHistoryList()
        {
            if (IsolatedStorage.FileExists("HistoryDB"))
            {
                IsolatedStorage.DeleteFile("HistoryDB");
            }
        }
    }
}
