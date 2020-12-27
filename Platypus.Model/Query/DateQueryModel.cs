using System;

namespace Platypus.Model.Query {

    public class DateQueryModel {
        public DateTime FromUtc { get; set; }

        public DateTime? ToUtc { get; set; }
    }
}