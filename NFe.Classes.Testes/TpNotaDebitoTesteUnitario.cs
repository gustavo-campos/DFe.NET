using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Informacoes.Identificacao;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Testes;

[TestClass]
public class TpNotaDebitoTesteUnitario
{
    [TestMethod(displayName: "Dado tpNFDebito 08 (desenquadramento do Simples Nacional), quando serializar e desserializar o ide, então o código 08 deve ser preservado.")]
    public void DadoTpNFDebito08QuandoSerializarEDesserializarEntaoOCodigoDeveSerPreservado()
    {
        var serializer = new XmlSerializer(typeof(ide));
        var origem = new ide
        {
            cUF = DFe.Classes.Entidades.Estado.SP,
            mod = DFe.Classes.Flags.ModeloDocumento.NFe,
            tpNF = TipoNFe.tnSaida,
            tpEmis = TipoEmissao.teNormal,
            tpAmb = DFe.Classes.Flags.TipoAmbiente.Homologacao,
            finNFe = FinalidadeNFe.fnNormal,
            tpNFDebito = TpNotaDebito.DesenquadramentoSimplesNacional,
        };

        string xml;
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, origem);
            xml = writer.ToString();
        }

        StringAssert.Contains(xml, "<tpNFDebito>08</tpNFDebito>");

        using var reader = new StringReader(xml);
        var lido = (ide)serializer.Deserialize(reader)!;
        Assert.AreEqual(TpNotaDebito.DesenquadramentoSimplesNacional, lido.tpNFDebito);
    }
}
