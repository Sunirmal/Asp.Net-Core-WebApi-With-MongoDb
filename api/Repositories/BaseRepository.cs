using System.Collections.Generic;
using api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositories
{
    public class BaseRepository<TModel> where TModel : BaseModel
    {
        protected readonly IMongoCollection<TModel> mongoCollection;

        public BaseRepository(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            mongoCollection = database.GetCollection<TModel>(collectionName);
        }

        public virtual List<TModel> GetList()
        {
            return mongoCollection.Find(row => true).ToList();
        }

        public virtual TModel GetById(string id)
        {
            var docId = new ObjectId(id);
            return mongoCollection.Find<TModel>(row => row.Id == docId).FirstOrDefault();
        }

        public virtual TModel Create(TModel model)
        {
            mongoCollection.InsertOne(model);
            return model;
        }

        public virtual bool Update(string id, TModel model)
        {
            var docId = new ObjectId(id);
            var replaceResult = mongoCollection.ReplaceOne(row => row.Id == docId, model);
            return replaceResult.ModifiedCount > 0;
        }

        public virtual void Delete(TModel model)
        {
            mongoCollection.DeleteOne(row => row.Id == model.Id);
        }

        public virtual bool Delete(string id)
        {
            var docId = new ObjectId(id);
            var result = mongoCollection.DeleteOne(row => row.Id == docId);
            return result.DeletedCount > 0;
        }
    }
}