using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Extentions
{
    public static class ModelStateChecker
    {
        public static bool CheckModelState( this object dto )
        {
            if (dto == null)
                return false;

            var properties = dto.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue( dto );
                if (property.PropertyType == typeof( string ))
                {
                    if (string.IsNullOrWhiteSpace( value as string ))
                        return false;
                }
                else if (property.PropertyType.IsValueType)
                {
                    var defaultValue = Activator.CreateInstance( property.PropertyType );
                    if (value == null || value.Equals( defaultValue ))
                        return false;
                }
                else
                {
                    if (value == null)
                        return false;
                }
            }
            return true;
        }
    }
}
