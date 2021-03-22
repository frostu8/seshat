using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOR_XML;

namespace Seshat.API
{
    public class DiceCardAbilityLocalizeRegistrar
    {
        private static SidModelRegistrar<BattleCardAbilityDesc> _registrar;
        private static SidModelRegistrar<BattleCardAbilityDesc> Registrar
        {
            get 
            { 
                if (_registrar == null) 
                    _registrar = new SidModelRegistrar<BattleCardAbilityDesc>(); 
                return _registrar; 
            }
        }

        internal static void Add(BattleCardAbilityDesc desc)
            => Registrar.Add(desc.GetSID(), desc);

        public static BattleCardAbilityDesc Get(string sid)
            => Registrar.Get(sid);
    }
}
