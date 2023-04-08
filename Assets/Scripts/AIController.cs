using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }
        [SerializeField] private AIBehaviour m_AIBehaviour;
        [SerializeField] private AIPointPatrol m_PatrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectMovePointTime;
        [SerializeField] private float m_FindNewTargetTime;
        [SerializeField] private float m_ShootDelay;
        [SerializeField] private float m_EvadeRayLength;

        private SpaceShip m_SpaceShip;

        private Vector3 m_MovePosition;
        [SerializeField] private GameObject m_MoveGameObject;
        private Projectile m_Projectile;

        private Destructible m_SelectedTarget;

        private Timer m_RandomizeDirectionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();
            m_Projectile = GetComponent<Projectile>();
            InitTimers();
        }
        private void Update()
        {
            UpdateTimers();

            UpdateAI();

            m_MoveGameObject.transform.position = m_MovePosition;
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviorPatrol();
            }
        }

        public void UpdateBehaviorPatrol()
        {
            ActionFindMovePosition();
            ActionControlShip();

            ActionFindNewAttackTarget();

            ActionFire();
            ActionEvadeCollision();
        }

        [SerializeField] private Vector2[] m_Point;
        private int m_RadiusPoints = 2;
        [SerializeField] private int m_Index;

        private Vector2 ActionFindNewMovePosition()
        {
            return m_Point[m_Index];
        }
        private void ActionFindMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MovePosition = MovePointFire.MakeLead(m_SelectedTarget.transform.localPosition);
                }
                else
                {
                    if (m_Point != null)
                    {
                        Vector2 newPoint = ActionFindNewMovePosition();
                        m_MovePosition = newPoint;
                        if (m_SpaceShip.transform.position.x >= m_MovePosition.x - m_RadiusPoints && m_SpaceShip.transform.position.y >= m_MovePosition.y - m_RadiusPoints &&
                            m_SpaceShip.transform.position.x <= m_MovePosition.x + m_RadiusPoints && m_SpaceShip.transform.position.y <= m_MovePosition.y + m_RadiusPoints)
                        {
                            m_Index++;
                            if (m_Index == m_Point.Length)
                            {
                                m_Index = 0;
                            }
                        }
                        return;
                    }
                    if (m_PatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;
                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;
                                m_MovePosition = newPoint;
                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }

                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }
        }

        


        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                m_MovePosition = transform.position + transform.right * 100.0f;
            }
        }
        private void ActionControlShip()
        {

            m_SpaceShip.ThrustControl = m_NavigationLinear;

            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormolized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;

        }

        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAliginTorqueNormolized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestractibleTarget();

                m_FindNewTargetTimer.Start(m_ShootDelay);
            }
        }
        private const int KILL_ZONE = 10;
        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                if(m_SpaceShip.transform.position.x - KILL_ZONE > m_SelectedTarget.transform.position.x||
                    m_SpaceShip.transform.position.x + KILL_ZONE < m_SelectedTarget.transform.position.x||
                    m_SpaceShip.transform.position.y - KILL_ZONE > m_SelectedTarget.transform.position.y ||
                    m_SpaceShip.transform.position.y + KILL_ZONE < m_SelectedTarget.transform.position.y)
                {
                    m_SelectedTarget = null;
                    return;
                }
                else
                {
                    if (m_FireTimer.IsFinished == true)
                    {
                        m_SpaceShip.Fire(TurretMode.Primary);

                        m_FindNewTargetTimer.Start(m_ShootDelay);
                    }
                }
            }
        }

        private Destructible FindNearestDestractibleTarget()
        {
            float maxDist = float.MaxValue;
            Destructible potentialTarget = null;
            foreach (var item in Destructible.AllDestractible)
            {
                if (item.GetComponent<SpaceShip>() == m_SpaceShip)
                {
                    continue;
                }
                if (item.TeamId == Destructible.TeamIdNeutral)
                {
                    continue;
                }
                if (item.TeamId == m_SpaceShip.TeamId)
                {
                    continue;
                }
                float dist = Vector2.Distance(m_SpaceShip.transform.position, item.transform.position);
                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = item;
                }

            }
            return potentialTarget;
        }


        #region Timers

        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }
        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }
        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }

        #endregion
    }
}

