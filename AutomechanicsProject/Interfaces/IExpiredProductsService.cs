using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с просроченными товарами
    /// </summary>
    public interface IExpiredProductsService
    {
        /// <summary>
        /// Выполняет автоматическое списание просроченных товаров
        /// </summary>
        int AutoWriteOffExpiredProducts();
    }
}
