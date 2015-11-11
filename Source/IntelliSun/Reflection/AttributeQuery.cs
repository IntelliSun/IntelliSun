using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IntelliSun.Collections;

namespace IntelliSun.Reflection
{
    public abstract class AttributeQuery : IEnumerable<Attribute>
    {
        private readonly Lazy<Attribute[]> attributes;

        protected AttributeQuery()
        {
            this.attributes = new Lazy<Attribute[]>(this.LoadAttributes);
        }

        public IEnumerable<Attribute> GetAttributes()
        {
            return this.Attributes;
        }

        public IEnumerable<TAttr> GetAttributes<TAttr>()
            where TAttr : Attribute
        {
            return this.Attributes.CastOrSkip<TAttr>();
        }

        public TAttr GetAttribute<TAttr>()
            where TAttr : Attribute
        {
            return (TAttr)this.Attributes.FirstOrDefault(attribute => attribute is TAttr);
        }

        public bool HasAttribute<TAttr>()
            where TAttr : Attribute
        {
            return this.GetAttributes<TAttr>().Any();
        }

        public TResult GetAttributeValue<TAttr, TResult>(Func<TAttr, TResult> selector)
            where TAttr : Attribute
        {
            var attribute = this.GetAttribute<TAttr>();
            return attribute != null ? selector(attribute) : default(TResult);
        }

        public IEnumerable<TResult> GetAttributesValues<TAttr, TResult>(Func<TAttr, TResult> selector)
            where TAttr : Attribute
        {
            return this.GetAttributes<TAttr>().Select(selector);
        }

        public IEnumerable<TResult> ConcatAttributesValues<TAttr, TResult>(Func<TAttr, IEnumerable<TResult>> selector)
            where TAttr : Attribute
        {
            return this.GetAttributes<TAttr>().SelectMany(selector);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<Attribute> GetEnumerator()
        {
            return this.Attributes.GetEnumerator();
        }

        protected abstract Attribute[] LoadAttributes();

        private IEnumerable<Attribute> Attributes
        {
            get { return this.attributes.Value; }
        }
    }
}