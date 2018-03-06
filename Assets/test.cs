using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yosh;
using UniRx;
public class test : MonoBehaviour
{
	[SerializeField] Button b1;
	[SerializeField] Button b2;
	[SerializeField] Button b3;
	[SerializeField] Button b4;
	[SerializeField] Button b5;

	public void Group1(bool active)
	{
		GroupType.Group1.SetEnable(active);
	}
	public void Group2(bool active)
	{
		GroupType.Group2.SetEnable(active);
	}
	public void Group3(bool active)
	{
		GroupType.Group3.SetEnable(active);
	}
	public void Group4(bool active)
	{
		GroupType.Group4.SetEnable(active);
	}
	public void Group5(bool active)
	{
		GroupType.Group5.SetEnable(active);
	}

	private void Start()
	{
		var sharedSource = GroupButtonSource<GroupType>.Instance[GroupType.Group2];
		b1.BindToOnClick(sharedSource, _ =>
		 {
			 return Observable.Timer(System.TimeSpan.FromSeconds(1)).AsUnitObservable();
		 }).AddTo(this);
		b2.BindToOnClick(sharedSource, _ =>
		{
			return Observable.Timer(System.TimeSpan.FromSeconds(2)).AsUnitObservable();
		}).AddTo(this);
		b3.BindToOnClick(sharedSource, _ =>
		{
			return Observable.Timer(System.TimeSpan.FromSeconds(3)).AsUnitObservable();
		}).AddTo(this);
		b4.BindToOnClick(sharedSource, _ =>
		{
			return Observable.Timer(System.TimeSpan.FromSeconds(4)).AsUnitObservable();
		}).AddTo(this);
		b5.BindToOnClick(sharedSource, _ =>
		{
			return Observable.Timer(System.TimeSpan.FromSeconds(5)).AsUnitObservable();
		}).AddTo(this);
	}

}
