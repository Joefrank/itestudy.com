using System.Data.Entity;

namespace elearning.data
{
    public class ELearningContextCustomInitializer : IDatabaseInitializer<DataDbContext>
    {
       
        public static void Initialize()
        {           
            using (var db = new DataDbContext())
            {
                {
                    db.Database.Initialize(true);
                }
            }
        }
        

        public void InitializeDatabase(DataDbContext context)
        {
            if (context.Database.Exists())
            {
                //this should be removed before deployment
#if DEBUG
                if (!context.Database.CompatibleWithModel(true))
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
#endif
            }
            else
            {
                context.Database.Create();
            }

            context.SaveChanges();
           
    
        }
    }
}
