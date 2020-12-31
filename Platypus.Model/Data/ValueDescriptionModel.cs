using System;
using System.Collections.Generic;
using System.Text;

namespace Platypus.Model.Data {

    public class ValueDescriptionModel<T1, T2> {
        public T1 Value { get; set; }
        public T2 Description { get; set; }
    }
}