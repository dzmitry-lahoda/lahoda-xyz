

`Error`s  are values.

It is enought to have `Error() String` to be `error`.

Default constuctors(errors.New, fmt.Errorf) make error which is just message.

So programmers can make their own error types with more context.

`errors.Unwrap` allows to get access to inner(underlying, source) error.

`errors.As` is shortcut to Go type cast.

`errors.Is` is shortcut for `==`.

`%w` format arg is shortcat which allows to wrap other error. `Is`/`As` compares each error in chain of `Unwrap`s.