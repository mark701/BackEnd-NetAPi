using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication3.Context;
using WebApplication3.InterFace;

namespace WebApplication3.Implemnetion
{
    public class DataBaseServiceImp<T> : IDataBaseService<T> where T : class

    {
        protected ApplicationDbContext _context;
        public DataBaseServiceImp(ApplicationDbContext context)
        {
            _context = context;

        }


        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) return false;
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteCondation(Expression<Func<T, bool>> Filter)
        {
            var entity = await _context.Set<T>().FindAsync(Filter);

            if (entity == null) return false;
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        //public List<T> Compare<T, TKey>(List<T> oldEntities, List<T> newEntities, Expression<Func<T, TKey>> propertySelector)
        //{
        //    try
        //    {

        //        var oldPropertyValues = oldEntities.Select(propertySelector.Compile()).ToList();
        //        var newPropertyValues = newEntities.Select(propertySelector.Compile()).ToList();

        //        // Find the property values that are in the old collection but not in the New collection
        //        var deleteResultIDs = oldPropertyValues.Except(newPropertyValues).ToList();

        //        // Filter the entities that need to be removed or updated (those that are in the new but not in the old)
        //        var entitiesToRemoveOrUpdate = oldEntities
        //            .Where(entity => deleteResultIDs.Contains(propertySelector.Compile().Invoke(entity)))
        //            .ToList();
        //        return entitiesToRemoveOrUpdate;


        //    }
        //    catch (Exception e)
        //    {
        //        return null;

        //    }

        //}

        public List<T> Compare<TKey>(List<T> oldEntities, List<T> newEntities, Expression<Func<T, TKey>> propertySelector)
        {
            try
            {
                var oldPropertyValues = oldEntities.Select(propertySelector.Compile()).ToList();
                var newPropertyValues = newEntities.Select(propertySelector.Compile()).ToList();

                // Find the property values that are in the old collection but not in the new collection
                var deleteResultIDs = oldPropertyValues.Except(newPropertyValues).ToList();

                // Filter the entities that need to be removed or updated (those that are in the old but not in the new)
                var entitiesToRemoveOrUpdate = oldEntities
                    .Where(entity => deleteResultIDs.Contains(propertySelector.Compile().Invoke(entity)))
                    .ToList();

                return entitiesToRemoveOrUpdate;
            }
            catch (Exception e)
            {
                // Log exception properly
                Console.WriteLine(e.Message);
                return new List<T>();
            }
        }

        public async Task<List<T>> DeleteRange(List<T> entities)
        {
            // Remove the entities from the DbContext
            // Fetch the entities with AsNoTracking (if needed) for improved performance when not modifying them
            var entitiesToDelete = await _context.Set<T>().Where(e => entities.Contains(e)) // Assuming you're deleting by matching entities
            .AsNoTracking().ToListAsync();
            _context.Set<T>().RemoveRange(entitiesToDelete);
            // Save changes to the database
            await _context.SaveChangesAsync();
            // Return the list of entities that were deleted
            return entities;
        }
        public async Task<T> Find(Expression<Func<T, bool>> criteria)
        {
            
                IQueryable<T> query = _context.Set<T>().Where(criteria);
                return await query.AsNoTracking().FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            return await query.ToListAsync<T>();
        }



        public async Task<List<T>> GetInclude(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
        public async Task<List<T>> GetIncludeWithCondition<TProperty>(
    Expression<Func<T, bool>> filter,
    params Expression<Func<T, TProperty>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(filter);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllInclude()
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            return await query.ToListAsync<T>();
        }
        public async Task<T> Save(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> SaveRange(List<T> entity)
        {

            await _context.Set<T>().AddRangeAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            await _context.Set<T>().AddRangeAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<string?> SaveImageAsync(IFormFile imageFile, string subFolder)

        {     string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };
               string MainFolder = "wwwroot"; 
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                    throw new ArgumentException("Invalid image file.");

                // Validate file extension
                var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
                if (!AllowedExtensions.Contains(fileExtension))
                    throw new ArgumentException("Only JPG and PNG images are allowed.");


                // Build relative folder path: wwwroot/Assets/{subFolder}
                var folderPath = Path.Combine(MainFolder, subFolder);
                Directory.CreateDirectory(folderPath);  // Ensure the folder exists

                // Generate unique file name
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var fullPath = Path.Combine(folderPath, uniqueFileName);

                // Save the image
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Return the relative path for easy access
                return Path.Combine(subFolder, uniqueFileName).Replace("\\", "/");
                //return uniqueFileName ;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving image: {ex.Message}");
                return null;
            }
        }
    }
}

