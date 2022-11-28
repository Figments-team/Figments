using System;
using System.Collections.Generic;

public class Tree<IDType, DataType>
{
    private class Node
    {
        private IDType _ID;
        public IDType ID => _ID;
        private DataType _Data;
        public DataType Data => _Data;
        private Node[] _Nodes;
        public Node[] Nodes => _Nodes;
        public int Children => _Nodes.Length;

        public Node(IDType id, DataType data)
        {
            _ID = id;
            _Data = data;
            _Nodes = new Node[0];
        }

        public Node GetChildNode(IDType id)
        {
            foreach(Node node in Nodes)
                if(node.ID.Equals(id))
                    return node;
            return null;
        }
    }
    
    private Node Head;
    public bool HasHead => Head != null;

    public Tree()
    {
        Head = null;
    }

    private Node GetNode(IDType[] path, uint offset = 0)
    {
        Node currentNode = Head;

        int index = 0;
        while(index < (path.Length - offset))
            currentNode = currentNode.GetChildNode(path[index++]);

        return currentNode;
    }

    public bool IsPathValid(IDType[] path)
    {
        return GetNode(path) != null;
    }

    private void SetNode(IDType[] path, DataType value)
    {
        Node nodeToSet = GetNode(path, 1);
        nodeToSet = new Node(path[path.Length - 1], value);
    }

    public DataType this[params IDType[] path]
    {
        get
        {
            return GetNode(path).Data;
        }
        set
        {
            SetNode(path, value);
        }
    }
}