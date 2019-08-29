using api.Entities;
using MongoDB.Driver;

namespace api.Repositories
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {}
        
        public virtual Book GetByTitle(string title)
        {
            return mongoCollection.Find<Book>(row => row.Title == title).FirstOrDefault();
        }
    }
}