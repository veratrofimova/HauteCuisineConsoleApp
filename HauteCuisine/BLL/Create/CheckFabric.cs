using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HauteCuisine.BLL.Create
{
    public class CheckFabric
    {
        private static Dictionary<string, Func<ICommonChecker>> fabrics = new Dictionary<string, Func<ICommonChecker>>()
        {
            { typeof(ClaimworkSendClaimLandProcess).Name, () => new LandChecker() },
            { typeof(ClaimworkSendClaimBuildingProcess).Name, () => new BuildingChecker() },
            { typeof(ClaimworkSendClaimChangeOfPurposeProcess).Name, () => new ChangePurposeChecker() },
            { typeof(ClaimworkSendClaimDKPProcess).Name, () => new DKPChecker() },

        };

        private Type ProcType { get; set; }

        public CheckFabric(Type procType)
        {
            this.ProcType = procType;
        }

        public ICommonChecker CreateChecker()
        {
            if (CheckFabric.fabrics.ContainsKey(this.ProcType.Name))
            {
                return CheckFabric.fabrics[this.ProcType.Name]();
            }

            throw new Exception("Фабрика не найдена!");
        }


    }
}
