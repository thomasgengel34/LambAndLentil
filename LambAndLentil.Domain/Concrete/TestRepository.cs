using System.Linq;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Domain.Concrete
{
    public class TestRepository<T > : JSONRepository<T >
        where T : BaseEntity, IEntity
    { 
        static string Folder { get; set; } 

        public TestRepository() : base()
        {
            char[] charsToTrim = { 'V', 'M' };
         Folder = typeof(T).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);

            // TODO: get relative path to work.  The first line works in testing but not in running it.
            // fullPath = @"../../../\LambAndLentil.Domain\App_Data\JSON\" + folder + "\\";
            FullPath = @" C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\" + Folder + "\\";
        } 
    }
}