using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections.Generic;

namespace Yosh
{
	enum GroupType
	{
		Group1,
		Group2,
		Group3,
		Group4,
		Group5
	}

	[RequireComponent(typeof(Button))]
	[DisallowMultipleComponent]
	public class GroupButton : MonoBehaviour
	{
		[SerializeField] GroupType m_groupType;
		private Button m_selfButton;
		
		void Awake()
		{
			m_selfButton = GetComponent<Button>();
			var command = GroupButtonSource<GroupType>.Instance.ToReactiveCommand(m_groupType);
			command.BindTo(m_selfButton);
		}
	}
}