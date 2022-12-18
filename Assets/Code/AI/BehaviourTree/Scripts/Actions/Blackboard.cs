using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [Serializable]
    public class Blackboard
    {
        private Dictionary<string, Variable> variableDictionary;

        public Blackboard()
        {
            variableDictionary = new Dictionary<string, Variable>();
        }
        
        #region Object Type

        public void AddVariable(string variableName, VariableType variableType, out Variable variable)
        {
            variable = null;
            if (!variableDictionary.ContainsKey(variableName))
            {
                switch (variableType)
                {
                    case VariableType.BOOL:
                        variable = new BoolVariable(variableName, false);
                        //variable = BoolVariable.Create(variableName, false);
                        break;
                    case VariableType.INT:
                        variable = new IntVariable(variableName, 0);
                        //variable = IntVariable.Create(variableName, 0);
                        break;
                    case VariableType.FLOAT:
                        variable = new FloatVariable(variableName, 0.0f);
                        //variable = FloatVariable.Create(variableName, 0.0f);
                        break;
                    case VariableType.STRING:
                        variable = new FloatVariable(variableName, 0.0f);
                        //variable = FloatVariable.Create(variableName, 0.0f);
                        break;
                    case VariableType.VECTOR3:
                        variable = new Vector3Variable(variableName, new Vector3());
                        //variable = Vector3Variable.Create(variableName, new Vector3(0,0,0));
                        break;
                    case VariableType.GAMEOBJECT:
                        variable = new GameObjectVariable(variableName, null);
                        //variable = GameObjectVariable.Create(variableName, null);
                        break;
                    default:
                        Debug.Log("Variable Type not supported.");
                        break;
                }

                if (variable != null)
                {
                    //variableDictionary.Add(new KeyValuePair<string, IVariable>(variableName, variable));
                    variableDictionary.Add(variableName, variable);
                }
            }
        }

        public void AddVariable(VariableInfo variableInfo)
        {
            if (!variableDictionary.ContainsKey(variableInfo.VariableName))
            {
                switch (variableInfo.VariableType)
                {
                    case VariableType.BOOL:
                        AddBoolVariable(variableInfo.VariableName, false);
                        break;
                    case VariableType.INT:
                        AddIntVariable(variableInfo.VariableName, 0);
                        break;
                    case VariableType.FLOAT:
                        AddFloatVariable(variableInfo.VariableName, 0);
                        break;
                    case VariableType.STRING:
                        AddStringVariable(variableInfo.VariableName, "");
                        break;
                    case VariableType.VECTOR3:
                        AddFloatVariable(variableInfo.VariableName, 0);
                        break;
                    case VariableType.GAMEOBJECT:
                        AddGameObjectVariable(variableInfo.VariableName, null);
                        break;
                    default:
                        Debug.Log("Variable Type not supported.");
                        break;
                }
            }
        }

        #endregion

        #region Int

        public void AddIntVariable(string variableName, int value)
        {
            if (!variableDictionary.ContainsKey(variableName))
            {
                variableDictionary.Add(variableName, new IntVariable(variableName, 0));
            }
        }

        public void AddIntVariable(string variableName, int startValue, out IntVariable variable)
        {
            variable = null;
            if (!variableDictionary.ContainsKey(variableName))
            {
                //variable = IntVariable.Create(variableName, startValue);
                variable = new IntVariable(variableName, startValue);
                if (variable != null)
                {
                    //variableDictionary.Add(new KeyValuePair<string, IVariable>(variableName, variable));
                    variableDictionary.Add(variableName, variable);
                }
            }
        }

        public void SetInt(string variableName, int value)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                IntVariable intVariable = (IntVariable) variable;
                intVariable.SetValue(value);
            }
            else
            {
                AddIntVariable(variableName, value);
            }
        }

        public int GetIntValue(string variableName)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                IntVariable intVariable = (IntVariable) variable;
                return (int) intVariable.Value;
            }
            else
            {
                AddIntVariable(variableName, 0);
            }

            return 0;
        }

        public bool TryGetIntVariable(string variableName, out IntVariable varResult)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                varResult = (IntVariable) variable;
                return true;
            }

            varResult = null;
            return false;
        }

        #endregion

        #region String

        public void SetStringValue(string variableName, string value)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                StringVariable stringVariable = (StringVariable) variable;
                stringVariable.SetValue(value);
            }
            else
            {
                AddStringVariable(variableName, "");
            }
        }

        public string GetStringValue(string variableName)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                StringVariable stringVariable = (StringVariable) variable;
                return (string) stringVariable.Value;
            }
            else
            {
                AddFloatVariable(variableName, 0.0f);
            }

            return "";
        }

        public void AddStringVariable(string variableName, string value)
        {
            if (!variableDictionary.ContainsKey(variableName))
            {
                variableDictionary.Add(variableName, new StringVariable(variableName, value));
            }
        }

        public void AddStringVariable(string variableName, string startValue, out StringVariable variable)
        {
            variable = null;
            if (!variableDictionary.ContainsKey(variableName))
            {
                variableDictionary.Add(variableName, new StringVariable(variableName, startValue));
            }
        }

        public bool TryGetStringVariable(string variableName, out StringVariable varResult)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                varResult = (StringVariable) variable;
                return true;
            }

            varResult = null;
            return false;
        }

        #endregion
        
        #region Float

        public void SetFloatValue(string variableName, float value)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                FloatVariable floatVariable = (FloatVariable) variable;
                floatVariable.SetValue(value);
            }
            else
            {
                AddFloatVariable(variableName, 0.0f);
            }
        }

        public float GetFloatValue(string variableName)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                FloatVariable floatVariable = (FloatVariable) variable;
                return (float) floatVariable.Value;
            }
            else
            {
                AddFloatVariable(variableName, 0.0f);
            }

            return 0.0f;
        }

        public void AddFloatVariable(string variableName, float value)
        {
            if (!variableDictionary.ContainsKey(variableName))
            {
                variableDictionary.Add(variableName, new FloatVariable(variableName, value));
            }
        }

        public void AddFloatVariable(string variableName, float startValue, out FloatVariable variable)
        {
            variable = null;
            if (!variableDictionary.ContainsKey(variableName))
            {
                variableDictionary.Add(variableName, new FloatVariable(variableName, startValue));
            }
        }

        public bool TryGetFloatVariable(string variableName, out FloatVariable varResult)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                varResult = (FloatVariable) variable;
                return true;
            }

            varResult = null;
            return false;
        }

        #endregion

        #region Bool

        public void SetBoolValue(string variableName, bool value)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                BoolVariable boolVariable = (BoolVariable) variable;
                boolVariable.SetValue(value);
            }
            else
            {
                AddBoolVariable(variableName, false);
            }
        }

        public bool GetBoolValue(string variableName)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                BoolVariable boolVariable = (BoolVariable) variable;
                return (bool) boolVariable.Value;
            }

            AddBoolVariable(variableName, false);
            return false;
        }

        public void AddBoolVariable(string variableName, bool value)
        {
            if (!variableDictionary.ContainsKey(variableName))
            {
                //variableDictionary.Add(new KeyValuePair<string, Variable>(variableName, BoolVariable.Create(variableName,value)));
                //variableDictionary.Add(new KeyValuePair<string, IVariable>(variableName, new BoolVariable(variableName,value)));
                //variableDictionary.Add(variableName, BoolVariable.Create(variableName,value));
            }
        }

        public void AddBoolVariable(string variableName, bool startValue, out BoolVariable variable)
        {
            variable = null;
            if (!variableDictionary.ContainsKey(variableName))
            {
                //variable = BoolVariable.Create(variableName, startValue);
                //variable = BoolVariable.Create(variableName, startValue);
                if (variable != null)
                {
                    //variableDictionary.Add(new KeyValuePair<string, IVariable>(variableName, variable));
                    variableDictionary.Add(variableName, variable);
                }
            }
        }

        public bool TryGetBoolVariable(string variableName, out BoolVariable varResult)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                varResult = (BoolVariable) variable;
                return true;
            }

            varResult = null;
            return false;
        }

        #endregion

        #region Vector3

        public void SetVector3KeyValue(string variableName, Vector3 value)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                Vector3Variable vector3Variable = (Vector3Variable) variable;
                vector3Variable.SetValue(value);
            }
            else
            {
                AddVector3Variable(variableName, new Vector3(0, 0, 0));
            }
        }

        public Vector3 GetVector3KeyValue(string variableName)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                Vector3Variable vector3Variable = (Vector3Variable) variable;
                return vector3Variable.Value;
            }
            else
            {
                AddVector3Variable(variableName, new Vector3(0, 0, 0));
            }

            return new Vector3(0, 0, 0);
        }

        public void AddVector3Variable(string variableName, Vector3 value)
        {
            if (!variableDictionary.ContainsKey(variableName))
            {
                //variableDictionary.Add(new KeyValuePair<string, Variable>(variableName, Vector3Variable.Create(variableName,value)));
                //variableDictionary.Add(new KeyValuePair<string, IVariable>(variableName, new Vector3Variable(variableName,value)));
                variableDictionary.Add(variableName, new Vector3Variable(variableName, value));
            }
        }

        public void AddVector3Variable(string variableName, Vector3 startValue, out Vector3Variable variable)
        {
            variable = null;
            if (!variableDictionary.ContainsKey(variableName))
            {
                //variable = Vector3Variable.Create(variableName, startValue);
                variable = new Vector3Variable(variableName, startValue);

                if (variable != null)
                {
                    //variableDictionary.Add(new KeyValuePair<string, IVariable>(variableName, variable));
                    variableDictionary.Add(variableName, variable);
                }
            }
        }

        public bool TryGetVector3Variable(string variableName, out Vector3Variable varResult)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                varResult = (Vector3Variable) variable;
                return true;
            }

            varResult = null;
            return false;
        }

        #endregion

        #region GameObject

        public void SetGameObjectValue(string variableName, GameObject value)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                GameObjectVariable gameObjectVariable = (GameObjectVariable) variable;
                gameObjectVariable.SetValue(value);
            }
            else
            {
                AddGameObjectVariable(variableName, null);
            }
        }

        public GameObject GetGameObjectValue(string variableName)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                GameObjectVariable gameObjectVariable = (GameObjectVariable) variable;
                return gameObjectVariable.Value;
            }
            else
            {
                AddGameObjectVariable(variableName, null);
            }

            return null;
        }

        public void AddGameObjectVariable(string variableName, GameObject value)
        {
            if (!variableDictionary.ContainsKey(variableName))
            {
                //variableDictionary.Add(new KeyValuePair<string, IVariable>(variableName, new GameObjectVariable(variableName,value)));
                variableDictionary.Add(variableName, new GameObjectVariable(variableName, value));
            }
        }

        public void AddGameObjectVariable(string variableName, GameObject startValue, out GameObjectVariable variable)
        {
            variable = null;
            if (!variableDictionary.ContainsKey(variableName))
            {
                variable = new GameObjectVariable(variableName, startValue);

                if (variable != null)
                {
                    //variableDictionary.Add(new KeyValuePair<string, IVariable>(variableName, variable));
                    variableDictionary.Add(variableName, variable);
                }
            }
        }

        public bool TryGetGameObjectVariable(string variableName, out GameObjectVariable varResult)
        {
            if (variableDictionary.TryGetValue(variableName, out Variable variable))
            {
                varResult = (GameObjectVariable) variable;
                return true;
            }

            varResult = null;
            return false;
        }

        #endregion
    }
}