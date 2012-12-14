using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Interfaces
{
    /// <summary>
    /// Interface used as a type of event listner.
    /// </summary>
    public interface IWeightHandler
    {
        /// <summary>
        /// Evaluates the data and returns it's weight
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        decimal Evaluate(object data);
    }
}
