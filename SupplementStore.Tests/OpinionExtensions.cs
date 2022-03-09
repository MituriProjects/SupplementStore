using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Tests {

    static class OpinionExtensions {

        public static Opinion WithOrderProductId(this Opinion opinion, OrderProductId orderProductId) {

            opinion.OrderProductId = orderProductId;

            return opinion;
        }

        public static Opinion WithText(this Opinion opinion, string text) {

            opinion.Text = text;

            return opinion;
        }

        public static Opinion WithGrade(this Opinion opinion, Grade grade) {

            opinion.Grade = grade;

            return opinion;
        }
    }
}
