% likes_both_mix(water,juice).
% true
% likes_both_mix(water, vodka).
% false 


likes(water).
likes(juice).

# likex both X and Y, X V Y
likes_both_mix(X,Y) :- likes(X) , likes(Y). 



