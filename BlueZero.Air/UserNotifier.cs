using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlueZero.Air.Data.Models;
using BlueZero.Air.Data.Services;

namespace BlueZero.Air
{
    public interface IUserNotifier
    {
        void NotifyParent(long childId, string message);
    }

    public class UserNotifier : IUserNotifier
    {
        private IParentService _parentService;

        public UserNotifier(IParentService parentService)
        {
            _parentService = parentService;
        }

        public void NotifyParent(long childId, string message)
        {
            Parent parent = _parentService.GetParentForChild(childId);
            if (parent != null)
            {
                NotifierHub.SendToUser(parent.UserProfile.UserId, message);
            }
        }
    }
}