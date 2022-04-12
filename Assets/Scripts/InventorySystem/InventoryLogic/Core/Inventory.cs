using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;

namespace InventorySystem
{
    public class Inventory
    {
        private readonly List<InventorySlot> m_Slots = new List<InventorySlot>();

        public uint SlotCount => (uint)m_Slots.Count;

        public delegate void SlotUpdateCallback(InventorySlot slot);
        public SlotUpdateCallback OnSlotAdded;
        public SlotUpdateCallback OnSlotRemoved;

        public InventorySlot CreateSlot()
        {
            InventorySlot newSlot = new InventorySlot();
            m_Slots.Add(newSlot);

            OnSlotAdded?.Invoke(newSlot);
            return newSlot;
        }

        public void DestroySlot(InventorySlot slot)
        {
            m_Slots.Remove(slot);
            OnSlotRemoved?.Invoke(slot);
        }

        public void Clear()
        {
            m_Slots.ForEach(slot => slot.Clear());
        }

        public void ForEach(Action<InventorySlot> action)
        {
            m_Slots.ForEach(slot => action(slot));
        }

        public InventorySlot FindFirst(Predicate<InventorySlot> predicate)
        {
            return m_Slots.Find(predicate);
        }

        public List<InventorySlot> FindAll(Predicate<InventorySlot> predicate)
        {
            return m_Slots.FindAll(predicate);
        }

        //does the serializing
        public string SerializeInventoryAsJSON()
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(m_Slots);
            }
            catch
            {
                throw new Exception();
            }
        }
        
        //does the deserializing
        public JsonDataObject[] GetSlotsFromJSON(string jsonString)
        {
            JsonDataObject[] jsonDataObject;
            try
            {
                jsonDataObject = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonDataObject[]>(jsonString);
            }
            catch
            {
                throw new Exception();
            }
            return jsonDataObject != null ? jsonDataObject : null;
        }

        //immutable json data struct
        public struct JsonDataObject
        {
            public JsonDataObject(string itemName, uint quantity)
            {
                this.itemName = itemName;
                this.quantity = quantity;
            }
            public string itemName {get;}
            public uint quantity {get;}

            internal void OnError(StreamingContext context, ErrorContext errorContext)
            {
                errorContext.Handled = true;
                throw errorContext.Error;
            }

        }
    }
}