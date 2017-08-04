using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.DocStore
{
    public interface IDocIndex<TDoc>
    {
        /// <summary>
        /// True, wenn unter der Id ein gültiges Dokument existiert
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool Exists(string Id);


        /// <summary>
        /// Erfasst ein Dokument zusammen mit einer Id in einem Index
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Document"></param>
        Task AddToIndexAsync(string Id, TDoc Document);


        /// <summary>
        /// Entfernt ein Dokument zusammen mit seiner Id aus einem Index
        /// </summary>
        /// <param name="Id"></param>
        Task RemoveFromIndexAsync(string Id);


        /// <summary>
        /// Alle vorausgegangenen Änderungen werden, falls noch nicht erfolgt
        /// persistiert
        /// </summary>
        Task SaveChangesAsync();


    }
}
