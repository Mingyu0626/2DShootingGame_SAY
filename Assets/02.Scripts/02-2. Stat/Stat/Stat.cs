public enum StatType
{
    Damage,
    Health,
    MoveSpeed,


    Count
}
// ������ �߻�ȭ => �츮�� ����� ������ �°� '�Ӽ�'�� '���'�� �и������ν� ����ȭ �ϴ� �۾�
// �Ӽ�:
//   - (Enum.StatType) ���� �̸�(ID)
//   - (int)           ����
//   - (float)         ���� ��ġ
//   - (int)           ���
//   - (float)         ���׷��̵� ��� ������
//   - (float)         ���׷��̵� ��ġ ������

// ���:
//   - ���׷��̵�: public bool TryUpgrade()
//   - ������ ���� ��ġ ���: private void Calculate()


public class Stat
{
    public StatType StatType;
    public int Level;

    private StatDataSO _data;

    public float Value;
    public int Cost;


    public Stat(StatType statType, int level, StatDataSO data)
    {
        StatType = statType;

        Level = level;

        _data = data;


        Calculate();
    }


    public bool TryUpgrade()
    {
        // 1. ���� ����Ѱ�?
        if (!CurrencyManager.Instance.TryConsume(CurrencyType.Gold, Cost))
        {
            return false;
        }

        // 2. ���� ����ϴٸ� ������!
        Level += 1;

        // 3. �������� ���� ��ġ/��� ����
        Calculate();

        return true;
    }

    private void Calculate()
    {
        Value = _data.DefaultValue + Level * _data.UpgradeAddValue;
        Cost = (int)(_data.DefaultCost + Level * _data.UpgradeAddCost);
    }
}