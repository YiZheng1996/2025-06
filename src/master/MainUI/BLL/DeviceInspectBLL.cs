﻿namespace MainUI.BLL
{
    public class DeviceInspectBLL
    {
        /// <summary>
        /// 获取设备检查列表
        /// </summary>
        /// <returns></returns>
        public List<DeviceInspectModel> GetDeviceInspects() =>
           VarHelper.fsql
          .Select<DeviceInspectModel>()
          .Where(x => x.IsDelete == 0)
          .ToList();

        /// <summary>
        /// 软删除设备检查记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelDeviceInspects(int id) => VarHelper.fsql
            .Update<DeviceInspectModel>()
            .Set(x => x.IsDelete, 1)
            .Set(x => x.DeleteTime, DateTime.Now)
            .Where(x => x.ID == id)
            .ExecuteAffrows() > 0;

        /// <summary>
        /// 新增设备检查记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDeviceInspect(DeviceInspectModel model) =>
            VarHelper.fsql.Insert(model)
            .ExecuteAffrows() > 0;

        /// <summary>
        /// 修改设备检查记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDeviceInspect(DeviceInspectModel model) =>
            VarHelper.fsql.Update<DeviceInspectModel>()
            .SetSource(model)
            .Where(x => x.ID == model.ID)
            .ExecuteAffrows() > 0;

        // 次数清零及时间清零
        public bool UpdateDATA(DeviceInspectModel model) => VarHelper.fsql
         .Update<DeviceInspectModel>()
         .SetIf(model.ActionNumber == 0, x => x.ActionNumber, 0)
         .SetIf(model.UseDuration == 0, x => x.UseDuration, 0)
         .Where(x => x.ID == model.ID)
         .ExecuteAffrows() > 0;
    }
}
