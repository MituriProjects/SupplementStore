﻿using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Tests {

    static class OpinionExtensions {

        public static Opinion WithOrderProductId(this Opinion opinion, OrderProduct orderProduct) {

            opinion.OrderProductId = orderProduct.OrderProductId;

            return opinion;
        }

        public static Opinion WithText(this Opinion opinion, string text) {

            opinion.Text = text;

            return opinion;
        }

        public static Opinion WithRating(this Opinion opinion, Rating rating) {

            opinion.Rating = rating;

            return opinion;
        }

        public static Opinion WithIsHidden(this Opinion opinion, bool value) {

            opinion.IsHidden = value;

            return opinion;
        }
    }
}
