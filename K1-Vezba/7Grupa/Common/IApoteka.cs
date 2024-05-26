using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IApoteka
    {
        [OperationContract]
        void DodajLek(Lek lek);
        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void ObrisiLek(int id);
        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void PromeniKolicinuLek(int id, int kolicina, bool povecaIliSmanji);
        [OperationContract]
        List<Lek> pronadjiLekNaOsnovuSastojka(string sastojak);
        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void dodajSastojak(int id,string sastojak);
        [OperationContract]
        List<Lek> dobaviSveLekove();
    }
}
