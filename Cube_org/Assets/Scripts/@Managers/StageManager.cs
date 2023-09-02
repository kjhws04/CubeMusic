using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] stageArray = null;
    GameObject currentStage;
    Transform[] stagePlates;

    [SerializeField] float offestY = -3f;
    [SerializeField] float plateSpeed = 10f;

    int stepCount = 0;
    int totalPlateCount = 0;

    public void RemoveStage()
    {
        if (currentStage != null)
            Destroy(currentStage);
    }

    public void SettingStage(int songNum)
    {
        stepCount = 0;
        currentStage = Instantiate(stageArray[songNum], Vector3.zero, Quaternion.identity);
        stagePlates = currentStage.GetComponent<Stage>().plates;
        totalPlateCount = stagePlates.Length;

        for (int i = 0; i < totalPlateCount; i++)
        {
            stagePlates[i].position = new Vector3(stagePlates[i].position.x,
                                                  stagePlates[i].position.y + offestY,
                                                  stagePlates[i].position.z);
        }
    }

    public void ShowNextPlate()
    {
        if (stepCount < totalPlateCount)
        {
            StartCoroutine(MovePlateCoroutine(stepCount++));
        }
    }

    IEnumerator MovePlateCoroutine(int p_num)
    {
        stagePlates[p_num].gameObject.SetActive(true);
        Vector3 destPos = new Vector3(stagePlates[p_num].position.x,
                                      stagePlates[p_num].position.y - offestY,
                                      stagePlates[p_num].position.z);

        while (Vector3.SqrMagnitude(stagePlates[p_num].position - destPos) >= 0.001f)
        {
            stagePlates[p_num].position = Vector3.Lerp(stagePlates[p_num].position, destPos, plateSpeed * Time.deltaTime);
            yield return null;
        }

        stagePlates[p_num].position = destPos;
    }
}
