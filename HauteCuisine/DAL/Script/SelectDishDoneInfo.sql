SELECT *
FROM "Сuisine"."DishInfo", "Сuisine"."DishDone"
where "Сuisine"."DishInfo"."Id" = "Сuisine"."DishDone"."IdDishInfo"