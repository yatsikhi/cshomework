using AvaloniaApplication1.Context;

using AvaloniaApplication1.Context;
using System;
using System.Linq;
using Tmds.DBus.Protocol;   

namespace AvaloniaApplication1.InsideClass
{
    public class GetTable
    {
        public string GetFruitsString()
        {
            using (var context = new PostgresContext())
            {
                var fruits = context.Products.ToList();
                return string.Join(Environment.NewLine, fruits.Select(f => $" {f.Id}. {f.Name}.{f.Type}"));
                
            }
        }
            
    }
    
}

