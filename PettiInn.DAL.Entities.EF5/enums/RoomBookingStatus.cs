using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.StringEnum;

namespace PettiInn.DAL.Entities.EF5.enums
{
    public enum RoomBookingStatus
    {
        [StringValue("已预订")]
        RoomBooked = 10,
        [StringValue("定金已付")]
        RoomBookedAndDepositPaied = 20,
        [StringValue("房费已到帐")]
        RoomBookedAndPaied = 30,
        [StringValue("预订已取消")]
        RoomBookingCanceled = 40
    }
}
