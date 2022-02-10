namespace DataStore;

using DataStore.Exceptions;
using Domain;
using System;
using System.Collections;
using System.Text.Json;
using System.Xml.Serialization;
public class InMemRepository : IRepository
{
	public Int32? CurrentIndex {get; private set;}
	private ArrayList _buf;

	public InMemRepository()
	{
		_buf = new ArrayList();
	}

	public void Add(IEntity entity)
	{
		CurrentIndex = _buf.Add(entity);
	}

	// TODO public void EditById(Guid ID, IEntity entity)
	public void Edit(IEntity entity)
	{
		// TODO find existing entity in the buffer.
		int idx = GetIndexByID(entity.GetId());
		// TODO handle not existant entity in buffer.
		if (idx == -1) throw new NotFoundException("Entity not found.");
		// TODO modification existant entity in buffer.
        try
        {
            entity.Validate();
            _buf.RemoveAt(idx);
            _buf.Add(entity);

        } catch (Exception e) 
        {
            throw e;
        }


	}

	public void Delete(IEntity entity)
	{
		_buf.Remove(entity);
	}


	public IEntity Get(IEntity entity)
	{
		// TODO find existing entity in the buffer.
		int idx = GetIndexByID(entity.GetId());
		// TODO handle not existant entity in buffer.
		if (idx == -1) throw new NotFoundException("Entity not found.");
		// TODO return the found entity.
        return (IEntity) _buf[idx];
	}

	public IEntity GetByEntityId(Guid Id)
	{
		// TODO find existing entity in the buffer.
        for(int i = 0; i < _buf.Count; i++) 
        {
            IEntity entity = (IEntity) _buf[i];
            if ( entity.GetId().Equals(Id)) 
            {
                return entity;
            }
        }
        throw new NotFoundException("Entity not found.");

        
		
	}

	public IEnumerable<IEntity> List()
	{
		return (IEnumerable<IEntity>)_buf.ToArray(typeof(IEntity));
	}

    //finds an item by its Guid; ensures that we will always find the item with teh corresponding Guid, 
    private Int32 GetIndexByID(Guid id) 
    {
        
        for(int i = 0; i < _buf.Count; i++) 
        {
            IEntity entity = (IEntity) _buf[i];
            if ( entity.GetId()== id) 
            {
                return i;
            }
        }
        return -1;
    }
}