//- Interop ------------------------------------------------------------------------------------------------------------
EXTERNAL chooseMapDestination       ()
EXTERNAL doSettleAndEndGame         ()
EXTERNAL gain                       (resource, amount)
EXTERNAL get                        (resource)
EXTERNAL lose                       (resource, amount)

//- Lists --------------------------------------------------------------------------------------------------------------
LIST Res                            =   Coins,
                                        Dust,
                                        Fairies,
                                        Fruit,
                                        Trinkets
