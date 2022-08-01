using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;
using UnityEngine;

namespace ModTheCube.Editor
{

    public class PreBuildProcessing : IPreprocessBuildWithReport
    {
        public int callbackOrder => 1;

        public void OnPreprocessBuild(BuildReport report)
        {
            System.Environment.SetEnvironmentVariable("EMSDK_PYTHON",
                "/Library/Frameworks/Python.framework/Versions/2.7/bin/python");
        }
    }
}