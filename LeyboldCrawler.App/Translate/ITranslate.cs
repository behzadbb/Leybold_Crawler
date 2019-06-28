using System;
using System.Collections.Generic;
using System.Text;
using LeyboldCrawler.Model.Translate;

namespace LeyboldCrawler.App.Translate
{
    public interface ITranslate:IDisposable
    {
        ResultTranslate Translate(InputTranslate input);
    }
}
