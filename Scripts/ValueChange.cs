using System;
public  class ValueChange
{
    void Start() {
        ValueChange<int> intValue = new ValueChange<int>();
        intValue.OnValueChange += OnValueChange;
    }
    void OnValueChange(){
        //Do Something
    }

}
public struct ValueChange<T>
{
   private T _mValue;
   public T MValue
   {
      get => _mValue;
      set
      {
         if (!value.Equals(_mValue))
            OnValueChange?.Invoke(value);
         _mValue = value;
      }
   }
   public event Action<T> OnValueChange;
}
