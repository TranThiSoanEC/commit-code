using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class KiemTraHoSoBenhAnBUS
    {
        private KiemTraHoSoBenhAnDAO ktDao;
        public KiemTraHoSoBenhAnBUS()
        {
            ktDao = new KiemTraHoSoBenhAnDAO();
        }


        public bool selectByMaBN(List<KiemTraHoSoBenhAnDTO> LichSuKB, KiemTraHoSoBenhAnDTO k)
        {
            return ktDao.selectByMaBN(LichSuKB, k);
        }
        public KiemTraHoSoBenhAnDTO selectCTPK(KiemTraHoSoBenhAnDTO k)
        {
            return ktDao.selectCTPK(k);
        }
        public bool selectCTTT(List<KiemTraHoSoBenhAnDTO> CTTT, KiemTraHoSoBenhAnDTO k)
        {
            return ktDao.selectCTTT(CTTT, k);
        }

    }
}