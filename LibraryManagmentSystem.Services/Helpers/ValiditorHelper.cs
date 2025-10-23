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
        public static void ValidateData( int? id, Object dto , String className )
        {
            if (dto.CheckModelState() == false)
                throw new ArgumentException( $"Invalid {className} data" );
            if (id.HasValue)
            {
                if (id <= 0)
                    throw new ArgumentException( $"Invalid {className} data" );
            }
        }
    }
}
