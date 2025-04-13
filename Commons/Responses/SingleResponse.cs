namespace Commons.Responses
{
    public class SingleResponse<T> : Response
    {
        public SingleResponse(string message, bool hasSuccess, T item, Exception? ex = null) : base(message, hasSuccess, ex)
        {
            Item = item;
        }

        public SingleResponse()
        {
        }

        public T? Item { get; set; }

        public bool NotFound
        { get { return EqualityComparer<T?>.Default.Equals(Item, default(T?)); } }
    }
}