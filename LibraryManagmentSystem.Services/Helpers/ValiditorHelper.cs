using LibraryManagmentSystem.Services.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Helpers
{
    public static class ValiditorHelper
    {
        public static void ValidateData( int? id, Object? dto , String className )
        {
            if (dto != null)
            {
                if (dto.CheckModelState() == false)
                    throw new ArgumentException( $"Invalid {className} data" );
            }
            
            if (id.HasValue)
            {
                if (id <= 0)
                    throw new ArgumentException( $"Invalid {className} data" );
            }
        }

        public static void ValidateId( int id, String className )
        {
            if (id <= 0)
                throw new ArgumentException( $"Invalid {className} Id" );
        }

        public static void EntityNotFoundCheck( Object entity, String className, int id )
        {
            if (entity == null)
                throw new KeyNotFoundException( $"{className} with id {id} not found." );
        }
    }
}
